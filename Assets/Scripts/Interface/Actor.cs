using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/*
#.스크립트 설명

- 
*/

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
    private int _pike = 0;
    private int _burnStack = 0;
    private int _selfBurnStack = 0;
    private int _venomStack = 0;
    private int _selfVenomStack = 0;
    private int _weakenStack = 0;
    private int _selfWeakenStack = 0;
    private int _reductionStack = 0;
    private int _selfReductionStack = 0;
    private int _fearStack = 0;
    private int _addictionStack = 0; 
    private int _nextTurnDamage = 0;
    private int _oneTimeReinforce = 0;
    private int _oneTimeProtect = 0;
    private int _oneTimeReduction = 0;
    private int _repeatStack = 1;
    private int _additionalDamage = 0;
    private int _additionalStack = 0;
    private bool _attackCount = false;

    private int[] _buffList;
    private int[] _debuffList;
    private int[] _allStateList;

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
            if(_burnStack < 0)
            {
                _burnStack = 0;
            }
            stateUIController.BurnOn(_burnStack);
        }
    }

    public int selfBurnStack
    {
        get { return _selfBurnStack; }
        set { _selfBurnStack = value; }
    }

    public int venomStack
    {
        get { return _venomStack; }
        set { _venomStack = value;
            if (_venomStack < 0)
            {
                _venomStack = 0;
            }
        }
    }
    public int selfVenomStack
    {
        get { return _selfVenomStack; }
        set { _selfVenomStack = value; }
    }

    public int weakenStack
    {
        get { return _weakenStack; }
        set
        {
            _weakenStack = value;
            if (_weakenStack < 0)
            {
                _weakenStack = 0;
            }
            stateUIController.WeakenOn(_weakenStack);
            Debug.Log("취약 스택" + _weakenStack);
        }
    }

    public int selfWeakenStack
    {
        get { return _selfWeakenStack; }
        set { _selfWeakenStack = value; }
    }

    public int reductionStack
    {
        get { return _reductionStack; }
        set
        {
            _reductionStack = value;
            if (_reductionStack < 0)
            {
                _reductionStack = 0;
            }
            stateUIController.ReductionOn(_reductionStack);
        }
    }

    public int selfReductionStack
    {
        get { return _selfReductionStack; }
        set { _selfReductionStack = value; }
    }

    public int fearStack
    {
        get { return _fearStack; }
        set { _fearStack = value; }
    }

    public int addictionStack
    {
        get { return _addictionStack; }
        set
        {
            _addictionStack = value;
            //stateUIController.AddictionOn(_addictionStack);
        }
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

    public int oneTimeProtect
    {
        get { return _oneTimeProtect; }
        set { _oneTimeProtect = value; }
    }

    public int oneTimeReduction
    {
        get { return _oneTimeReduction; }
        set { _oneTimeReduction = value; }
    }

    public int additionalDamage
    {
        get { return _additionalDamage; }
        set { _additionalDamage = value;
            stateUIController.PowerOn(_additionalDamage);
        }
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

    public int[] debuffList
    {
        get { return _debuffList; }
        set { _debuffList = value; }
    }

    public int[] allStateList
    {
        get { return _allStateList; }
        set { _allStateList = value; }
    }

    #endregion

    /*==================================================================================================================================*/

    private void OnEnable()
    {
        deck = GetComponent<Deck>();
        hand = GetComponent<Hand>();

        buffList = new int[] { additionalStack, additionalDamage, pike };
        debuffList = new int[] { burnStack, venomStack, reductionStack, weakenStack };
        allStateList = new int[] { protect, oneTimeProtect, additionalStack, additionalDamage, oneTimeReinforce, pike,
                                   burnStack, venomStack, reductionStack, weakenStack };
        garbageField.InitDeck();
    }

    private void StackInit()
    {
        tension = 0;
        heal = 0;
        damage = 0;
        tension = 0;
        repeatStack = 1;
        additionalDamage = nextTurnDamage;
        nextTurnDamage = 0;
        additionalStack = 0;
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
        if (burnStack > 0)
        {
            Damaged(this, burnStack * 2, DamageType.Burn);
            burnStack -= 1;
        }
        if (pike > 0)
        {
            pike = 0;
        }
        if (venomStack > 0)
        {
            Damaged(this, venomStack * 2, DamageType.Burn);
            venomStack = Mathf.FloorToInt(venomStack);
        }
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

            target.Damaged(this, damage, DamageType.Beat);

            tensionManager.tension += tension;

            #region 디버깅용 임시 로그
            Debug.Log(target.gameObject.name + " 체력 : " + target.hp);
            Debug.Log("입히는 데미지 : " + damage);
            Debug.Log("상태 방어력 : " + protect);
            Debug.Log(target.gameObject.name + "화염 스택 : " + target.burnStack);
            #endregion

            if (target.attackCount == true)
            {
                Damaged(target, target.pike, DamageType.Beat);
            }

            target.attackCount = false;
        }
    }

    public virtual void Damaged(Actor attacker, int _damage, DamageType _type)
    {
        if (_damage <= 0) return;

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

                totalDamage += attacker.additionalDamage + attacker.oneTimeReinforce + weakenStack + (addictionStack * 5) - reductionStack;
                
                if (weakenStack > 0) weakenStack -= 1;

                tension += (addictionStack * 5);

                attacker.oneTimeReinforce = 0;
                attacker.oneTimeReduction = 0;

                if (fearStack <= 10)
                {
                    totalDamage = (int)(totalDamage * (1 - (fearStack * 0.1f)));
                }
                else
                {
                    totalDamage = 0;
                }



                if (fearStack <= 10)
                {
                    totalDamage = (int)(totalDamage * (1 - (fearStack * 0.1f)));
                }
                else
                {
                    totalDamage = 0;
                }

                break;
        }

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
