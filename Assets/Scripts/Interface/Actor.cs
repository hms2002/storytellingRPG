using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public enum DamageType
{
    Burn,
    Venom,
    Beat
}

public class Actor : MonoBehaviour
{
    [Header("상태창 UI")]
    public ActorStateUIControler stateUIController;
    public readonly CharactorState charactorState = new CharactorState();



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
    private int _tension = 0;

    [Header("최대 체력")]
    [SerializeField] protected int _MAX_HP = 100;
    private int _hp = 100;
    private int _protect = 0;
    private int _heal = 0;
    private int _damage = 0;
    private int _repeatStack = 1;
    private int _additionalDamage = 0;
    private int _additionalStack = 0;
    private bool _attackCount = false;

    private int[] buffList;
    private int[] debuffList;
    private int[] allStateList;

    public int tension
    {
        get { return _tension; }
        set { _tension = value; }
    }

    public int MAX_HP
    {
        get { return _MAX_HP; }
        set { _MAX_HP = value; }
    }

    public int hp
    {
        get { return _hp; }
        set {
            _hp = value;
            if (_hp > _MAX_HP)
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

    public int heal
    {
        get { return _heal; }
        set { _heal = value; }
    }

    public int damage
    {
        get { return _damage; }
        set 
        { _damage = value;
            if (_damage < 0)
            {
                _damage = 0;
            }
        }
    }

    public int repeatStack
    {
        get { return _repeatStack; }
        set { _repeatStack = value; }
    }

    public int additionalDamage
    {
        get { return _additionalDamage; }
        set { _additionalDamage = value;
            charactorState.AddState(StateDatabase.stateDatabase.reinforce, 
                _additionalDamage);
        }
    }


    public bool attackCount
    {
        get { return _attackCount; }
        set { _attackCount = value; }
    }


    #endregion

    /*==================================================================================================================================*/

    private void OnEnable()
    {
        deck = GetComponent<Deck>();
        hand = GetComponent<Hand>();

        buffList = new int[(int)BuffType.Size] ;

        debuffList = new int[(int)DebuffType.Size];

        allStateList = new int[(int)StateType.Size]; 
        garbageField.InitDeck();

        charactorState.Init(stateUIController);
    }

    private void StackInit()
    {
        tension = 0;
        heal = 0;
        damage = 0;
        tension = 0;
        repeatStack = 1;
        additionalDamage = charactorState.GetStateStack(StateType.nextTurnDamage);
        charactorState.ResetState(StateType.nextTurnDamage);
    }

    public virtual void BeforeAction()
    {
        // Actor가 Keyword를 안 뽑았다면
        if (hasActorDrawnKeywords == false)
        {
            deck.ShuffleDeck();
            FillSupHandInfo();
            FillMainHandInfo();

            // Actor의 Hand가 다 채워졌으니 True로 설정
            hasActorDrawnKeywords = true;
        }
        
        StackInit();
    }

    public void StartTurn()
    {
        #region 턴중 버프, 디버프 관리
        charactorState.StartTurnDamage(this);
        /*if (burnStack > 0)
        {
            Damaged(this, burnStack * 2);
            burnStack -= 1;
        }
        if (pike > 0)
        {
            pike = 0;
        }
        if (venomStack > 0)
        {
            Damaged(this, venomStack * 2);
            venomStack = Mathf.FloorToInt(venomStack);
        }
        int addictionStack = (int)charactorState.GetState(StateType.addiction);
        if (addictionStack > 0)
        {
            Damaged(this, addictionStack * 2, DamageType.Burn);
        }
        */
        #endregion
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

    public void GetKeywordSup(KeywordSup _keywordSup)
    {
        /*if (Resources.Load<GameObject>("Asset/Prefabs/MonsterKeywords/ToxicSlime/SupKeyword/Addicted") == _keywordSup
            && addictionStack == 0) return;*/

        // Support 키워드를 사용
        keywordSup = _keywordSup;

        AddToSupGarbageField();

        ShowKeywordMain();
    }

    public void GetKeywordMain(KeywordMain _keywordMain)
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
        for (int i = 0; i < hand.HANDSIZE; i++)
        {
            garbageField.AddMainKeywordOnDeck(hand.ThrowMainKeyword(0));
        }
    }

    public virtual void Action(Actor target)
    {
        keywordSup.Check(keywordMain);
        keywordMain.Check(keywordSup);

        keywordSup.Execute(this, target);
        keywordMain.Execute(this, target);
        Execute(target);
    }

    public void Execute(Actor target)
    {
        for (int i = 1; i <= repeatStack; i++)
        {
            TensionManager tensionManager = TensionManager.tensionManagerUI;

            target.Damaged(this, damage);

            tensionManager.tension += tension;

            if (target.attackCount == true)
            {
                Damaged(target, target.charactorState.GetStateStack(StateType.pike));
            }

            target.attackCount = false;
        }
    }

    public virtual void Damaged(Actor attacker, int _damage)
    {
        if (_damage <= 0) return;

        int totalDamage = _damage;

        if(attacker == this)
        {
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
            return;
        }
        else
        {
            totalDamage += attacker.additionalDamage
                + attacker.charactorState.GetStateStack(StateType.oneTimeReinforce)
                + charactorState.GetStateStack(StateType.weaken)
                - charactorState.GetStateStack(StateType.reduction);
        
            int fearStack = attacker.charactorState.GetStateStack(StateType.fear);
            if (fearStack <= 10)
            {
                totalDamage = (int)(totalDamage * (1 - (fearStack * 0.1f)));
            }
            else
            {
                totalDamage = 0;
            }

            int oneTimeProtect = attacker.charactorState.GetStateStack(StateType.oneTimeProtect);
            if (oneTimeProtect > 0)
            {
                if (oneTimeProtect < totalDamage)
                {
                    totalDamage -= oneTimeProtect;
                    oneTimeProtect = 0;
                }
                else
                {
                    oneTimeProtect -= totalDamage;
                    totalDamage = 0;
                    oneTimeProtect = 0;
                }
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
            attacker.charactorState.ReductionOnAttack();
            charactorState.ReductionOnDamaged();
        }
    }



    public void SelectKeyword()
    {
        ShowKeywordSup();
    }

    private void ShowKeywordSup()
    {
        hand.SubstantiateSupKeywordData();
    }

    private void ShowKeywordMain()
    {
        hand.SubstantiateMainKeywordData();
    }
}
