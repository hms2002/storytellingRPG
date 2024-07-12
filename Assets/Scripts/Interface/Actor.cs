using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public enum DamageType
{
    Burn,
    Beat
}

public class Actor : MonoBehaviour
{
    [Header("상태창 UI")]
    public ActorStateUIControler stateUIController;

    #region Actor의 키워드 관련 변수
    private Deck deck;                          // Actor가 갖고 있는 "기본"덱 (Support, Main 키워드)
    private Hand hand;
    private Deck garbageField = new Deck();     // Actor가 갖고 있는 "무덤"덱 (Support, Main 키워드)
        
    private KeywordSup _keywordSup;
    private KeywordMain _keywordMain;

    public KeywordSup keywordSup
    {
        get { return _keywordSup; }
        set { _keywordSup = value; }
    }
    public KeywordMain keywordMain
    {
        get { return _keywordMain; }
        set { _keywordMain = value; }
    }

    private bool hasActorDrawnKeywords = false;         // Actor의 키워드 드로우 여부 확인용
    #endregion

    #region Actor의 능력치 관련 변수, 함수
    protected int _MAX_HP = 100;
    private int _hp = 100;
    private int _protect = 0;
    private int _pike = 0;
    private int _burnStack = 0;
    private int _weakenStack = 0;
    private int _reductionStack = 0;
    private int _nextTurnDamage = 0;
    private int _additionalDamage = 0;
    private int _additionalStack = 0;
    private bool _attackCount = false;

    public int MAX_HP
    {
        get { return _MAX_HP; }
    }

    public int hp
    {
        get { return _hp; }
        set { 
                _hp = value; 
                if(_hp > _MAX_HP)
                {
                    _hp = _MAX_HP;
                }
                if (_hp < 0)
                {
                    _hp = 0;
                }
                stateUIController.UpdateHpUI(_hp, MAX_HP);
            }
    }
    
    public int protect
    {
        get { return _protect; }
        set 
        { 
            _protect = value;
            stateUIController.ProtectOn(_protect);
        }
    }
    
    public int pike
    {
        get { return _pike; }
        set { _pike = value; }
    }
    
    public int burnStack
    {
        get { return _burnStack; }
        set
        {
            _burnStack = value;
            stateUIController.BurnOn(_burnStack);
        }
    }
    
    public int weakenStack
    {
        get { return _weakenStack; }
        set
        {
            _weakenStack = value;
            stateUIController.WeakenOn(_weakenStack);
            Debug.Log("취약 스택" + _weakenStack);
        }
    }
    
    public int reductionStack
    {
        get { return _reductionStack; }
        set
        {
            _reductionStack = value;
            stateUIController.ReductionOn(_reductionStack);
        }
    }

    public int nextTurnDamage
    {
        get { return _nextTurnDamage; }
        set { _nextTurnDamage = value; }
    }

    public int additionalDamage
    {
        get { return _additionalDamage; }
        set { _additionalDamage = value; }
    }
    
    public int additionalStack
    {
        get { return _additionalStack; }
        set
        {
            _additionalStack = value;
            stateUIController.ReductionOn(_additionalStack);
        }
    }

    public bool attackCount
    {
        get { return _attackCount; }
        set { _attackCount = value; }
    }
    #endregion


    private void OnEnable()
    {
        deck = GetComponent<Deck>();
        hand = GetComponent<Hand>();

        garbageField.InitDeck();

    }

    public virtual void BeforeAction()
    {
        // Actor가 Keyword를 안 뽑았다면
        if (hasActorDrawnKeywords == false)
        {
            FillSupHandInfo();
            FillMainHandInfo();

            // Actor의 Hand가 다 채워졌으니 True로 설정
            hasActorDrawnKeywords = true;
        }
        
        if (burnStack > 0)
        {
            Damaged(burnStack * 2, DamageType.Burn);
            burnStack -= 1;
        }
    }

    private void FillSupHandInfo()
    {
        // Keyword 드로우 3번 반복
        for (int i = 0; i < hand.HANDSIZE; i++)
        {
            // Support덱이 비어있다면
            if (deck.IsSupDeckEmpty())
            {
                // 무덤덱에서 카드를 꺼내와 Support덱을 초기화
                for (int j = 0; j < garbageField.GetSupDeckSize(); j++)
                {
                    deck.AddSupKeywordOnDeck(garbageField.DrawSupKeyword());
                }
            }

            // Support덱에서 1장 랜덤 드로우
            hand.SetSupPrefabInfo(deck.DrawSupKeyword());
        }
    }

    private void FillMainHandInfo()
    {
        // Keyword 드로우 3번 반복
        for (int i = 0; i < hand.HANDSIZE; i++)
        {
            // Main덱이 비어있다면
            if (deck.IsMainDeckEmpty())
            {
                // 무덤덱에서 카드를 꺼내와 Main덱을 초기화
                for (int j = 0; j < garbageField.GetMainDeckSize(); j++)
                {
                    deck.AddMainKeywordOnDeck(garbageField.DrawMainKeyword());
                }
            }

            // Main덱에서 각각 1장 랜덤 드로우                
            hand.SetMainPrefabInfo(deck.DrawMainKeyword());
        }
    }

    internal void GetKeywordSup(KeywordSup _keywordSup)
    {
        // Support 키워드를 사용
        keywordSup = _keywordSup;

        AddToSupGarbageField();

        ShowKeywordMain();
    }

    internal void GetKeywordMain(KeywordMain _keywordMain)
    {
        // Main 키워드를 사용
        keywordMain = _keywordMain;

        AddToMainGarbageField();

        // Actor의 Hand가 비었으니 false로 설정
        hasActorDrawnKeywords = false;
    }

    private void AddToSupGarbageField()
    {
        // HANDSIZE만큼 반복하여 사용한 Support 키워드 + 나머지 Support 키워드 무덤덱으로 이동
        for (int i = 0; i < hand.HANDSIZE; i++)
        {
            garbageField.AddSupKeywordOnDeck(hand.ThrowSupKeyword(0));
        }
    }

    private void AddToMainGarbageField()
    {
        // HANDSIZE만큼 반복하여 사용한 Main 키워드 + 나머지 Main 키워드 무덤덱으로 이동
        for (int i = 0; i<hand.HANDSIZE; i++)
        {
            garbageField.AddMainKeywordOnDeck(hand.ThrowMainKeyword(0));
        }
    }

    internal virtual void Action(Actor target)
    {
        Sentence sentence = new Sentence();

        keywordSup.Check(keywordMain);
        keywordMain.Check(keywordSup);

        protect = 0;
        additionalDamage += nextTurnDamage;

        keywordSup.Execute(this, target, sentence);
        keywordMain.Execute(this, target, sentence);
        sentence.execute(this, target);
    }

    public virtual void Damaged(int _damage, DamageType _type)
    {
        if (_damage <= 0)
            return;
        int totalDamage = _damage;
        switch(_type)
        {
            case DamageType.Burn:
                Debug.Log(gameObject.name + "화염 피해" + _damage);
                break;

            case DamageType.Beat:
                Debug.Log(gameObject.name + "타격 피해" + _damage);
                if (totalDamage > 0)
                {
                    attackCount = true;
                }
                if (additionalDamage > 0)
                {
                    totalDamage += additionalDamage;
                    additionalDamage = 0;
                }
                if (weakenStack > 0)
                {
                    totalDamage += weakenStack;
                    weakenStack -= 1;
                }
                if(reductionStack > 0)
                {
                    if(totalDamage < reductionStack)
                    {
                        totalDamage = 0;
                        reductionStack -= 1;
                    }
                    else
                    {
                        totalDamage -= reductionStack;
                        reductionStack -= 1;
                    }
                }
                break;
        }
        if (protect > 0)
        {
            if (protect < totalDamage)
            {
                totalDamage -= protect;
                protect = 0;
            }
            else
            {
                protect -= totalDamage;
                totalDamage = 0;
            }
        }
        hp -= totalDamage;
    }

    public void StartTurn()
    {
        ShowKeywordSup();
    }

    private void ShowKeywordMain()
    {
        hand.FillMainHand();
    }

    private void ShowKeywordSup()
    {
        hand.FillSupHand();
    }
}
