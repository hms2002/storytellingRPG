using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    public Slider hpSlider;
    public TextMeshProUGUI hpText;

    #region Actor의 키워드 관련 변수
    [Header("덱 오브젝트")]
    [SerializeField] private Deck deck;          // Actor가 갖고 있는 "기본"덱 (Support, Main 키워드)
    private Deck garbageField = new Deck();     // Actor가 갖고 있는 "무덤"덱 (Support, Main 키워드)

    [Header("키워드 캔버스")]
    public GameObject supCanvas;                // Support 키워드를 담고 있는 캔버스
    public GameObject mainCanvas;               // Main 키워드를 담고 있는 캔버스

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

    private List<GameObject> supportHand = new List<GameObject>();      // Actor의 SupportHand 리스트
    private List<GameObject> mainHand = new List<GameObject>();         // Actor의 MainHand 리스트

    [Header("손 패 사이즈")]
    [SerializeField] private const int HANDSIZE = 3;    // Actor가 가져야 하는 Hand 개수
    private bool hasActorDrawnKeywords = false;         // Actor의 키워드 드로우 여부 확인용
    #endregion

    #region Actor의 능력치 관련 변수, 함수
    protected int _MAX_HP = 100;
    private int _hp = 100;
    private int _protect = 0;
    private int _pike = 0;
    private int _burnStack = 0;
    private int _venomStack = 0;
    private int _weakenStack = 0;
    private int _reductionStack = 0;
    private int _nextTurnDamage = 0;
    private int _oneTimeReinforce = 0;
    private int _additionalDamage = 0;
    private int _additionalStack = 0;
    private bool _attackCount = false;

    private int[] _buffList;

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

    public int venomStack
    {
        get { return _venomStack; }
        set {_venomStack = value; } 
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
    public int oneTimeReinforce
    {
        get { return _oneTimeReinforce; }
        set { _oneTimeReinforce = value; }
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

    public int[] buffList
    {
        get { return _buffList; }
        set { _buffList = value; }
    }


    #endregion

    private void Start()
    {
        buffList = new int[] { burnStack, venomStack, reductionStack, weakenStack, pike };
        garbageField.InitDeck();
    }

    public virtual void BeforeAction()
    {
        // Actor가 Keyword를 안 뽑았다면
        if (hasActorDrawnKeywords == false)
        {
            FillSupHand();
            FillMainHand();

            // Actor의 Hand가 다 채워졌으니 True로 설정
            hasActorDrawnKeywords = true;
        }
        #region 턴중 버프, 디버프 관리
        if (burnStack > 0)
        {
            Damaged(this, burnStack * 2, DamageType.Burn);
            burnStack -= 1;
        }
        if (additionalDamage > 0)
        {
            additionalDamage = 0;
        }
        if(pike > 0)
        {
            pike = 0;
        }
        if(venomStack > 0)
        {
            Damaged(this, venomStack * 2, DamageType.Burn);
            venomStack = Mathf.FloorToInt(venomStack);
        }
        #endregion
    }

    private void FillSupHand()
    {
        // Keyword 드로우 3번 반복
        for (int i = 0; i < HANDSIZE; i++)
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
            supportHand.Add(deck.DrawSupKeyword());
        }
    }

    private void FillMainHand()
    {
        // Keyword 드로우 3번 반복
        for (int i = 0; i < HANDSIZE; i++)
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
            mainHand.Add(deck.DrawMainKeyword());
        }
    }

    internal void GetKeywordSup(KeywordSup _keywordSup)
    {
        // Support 키워드를 사용
        keywordSup = _keywordSup;

        // HANDSIZE만큼 반복하여 사용한 Support 키워드 + 나머지 Support 키워드 무덤덱으로 이동
        for (int i = 0; i < HANDSIZE; i++)
        {
            garbageField.AddSupKeywordOnDeck(supportHand[i]);
            supportHand[i].SetActive(false);
        }

        // Support 키워드 리스트 초기화
        supportHand.Clear();

        ShowKeywordMain();
    }

    internal void GetKeywordMain(KeywordMain _keywordMain)
    {
        // Main 키워드를 사용
        keywordMain = _keywordMain;

        // HANDSIZE만큼 반복하여 사용한 Main 키워드 + 나머지 Main 키워드 무덤덱으로 이동
        for (int i = 0; i < HANDSIZE; i++)
        {
            garbageField.AddMainKeywordOnDeck(mainHand[i]);
            mainHand[i].SetActive(false);
        }

        // Main 키워드 리스트 초기화
        mainHand.Clear();

        // Actor의 Hand가 비었으니 false로 설정
        hasActorDrawnKeywords = false;
        mainCanvas.SetActive(false);
        Debug.Log("hasActorDrawnKeywords" + hasActorDrawnKeywords);
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

    public virtual void Damaged(Actor attacker, int _damage, DamageType _type)
    {
        if (_damage <= 0)
            return;
        int totalDamage = _damage;
        switch(_type)
        {
            case DamageType.Burn:
                Debug.Log(gameObject.name + "화염 피해" + _damage);
                break;
            case DamageType.Venom:
                Debug.Log(gameObject.name + "맹독 피해" + _damage);
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
                }
                if (oneTimeReinforce > 0)
                {
                    totalDamage += oneTimeReinforce;
                    oneTimeReinforce = 0;
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

        // 체력 UI 조정
        hpSlider.value = hp/(float)MAX_HP;
        hpText.text = hp + " / " + MAX_HP;
    }

    public void StartTurn()
    {
        ShowKeywordSup();
    }

    private void ShowKeywordMain()
    {
        supCanvas.SetActive(false);
        mainCanvas.SetActive(true);

        for (int i = 0; i < HANDSIZE; i++)
        {
            mainHand[i].SetActive(true);
        }
    }

    private void ShowKeywordSup()
    {
        supCanvas.SetActive(true);

        for (int i = 0; i < HANDSIZE; i++)
        {
            supportHand[i].SetActive(true);
        }
    }
}
