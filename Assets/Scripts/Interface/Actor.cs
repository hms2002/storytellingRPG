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

    public string _attackSound = "타격음_주먹2";

    internal void AddSupKeywordToOriginalDeck(GameObject keywordSup)
    {
        OriginalDeck.AddSupKeywordOnDeck(keywordSup);
    }
    public void AddMainKeywordToOriginalDeck(GameObject keywordMain)
    {
        OriginalDeck.AddMainKeywordOnDeck(keywordMain);
    }

    #region Actor의 키워드 관련 변수
    private Deck OriginalDeck;
    private Deck deck;                             // Actor가 갖고 있는 "기본"덱 (Support, Main 키워드)
    private Hand hand;
    private Deck garbageField = new Deck();        // Actor가 갖고 있는 "무덤"덱 (Support, Main 키워드)

    [Header("덱 정보 피봇")]
    private DeckInfoPivot deckInfoPivot;           //
    private DeckInfoPivot garbageFieldInfoPivot;   //

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
    #endregion

    #region Actor의 능력치 관련 변수, 함수
    private int _tension = 0;

    [Header("최대 체력")]
    [SerializeField] protected int _MAX_HP = 100;
    private int _hp;
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


    public string attackSound
    {
        get { return _attackSound; }
        set { _attackSound = value; }
    }
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
            if(charactorState != null)
                if (charactorState.GetStateStack(StateType.ore) != 0 && value < _hp)
                    charactorState.ReductionByValue(StateType.ore, _hp - value);

            _hp = value;
            if (_hp > _MAX_HP)
            {
                _hp = _MAX_HP;
            }
            if (_hp < 0)
            {
                _hp = 0;
            }
            if(stateUIController != null)
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
        // 원본 덱 가져오기 전에 있는지 확인
        if((int)transform.childCount >= 2)
            OriginalDeck = transform.GetChild(1).GetComponent<Deck>();

        deck = GetComponent<Deck>();
        hand = GetComponent<Hand>();

        // 원본 덱 없으면 복사 X
        if (OriginalDeck != null)
        {
            Debug.Log(OriginalDeck.gameObject.name);
            deck.InitDeck(OriginalDeck);
        }

        garbageField.InitDeck();

        charactorState.Init(stateUIController);

        _hp = _MAX_HP;
        _protect = 0;
        _heal = 0;
        _damage = 0;
        _repeatStack = 1;
        _additionalDamage = 0;
        _additionalStack = 0;
        _attackCount = false;
    }

    private void StackInit()
    {
        tension = 0;
        heal = 0;
        damage = 0;
        tension = 0;
        repeatStack = 1;
    }

    public virtual void BeforeAction()
    {
        // 씬에 존재하는 DeckPivot 태그의 오브젝트 찾기
        GameObject[] pivotTemp = GameObject.FindGameObjectsWithTag("DeckPivot");

        // 찾은 DeckPivot 오브젝트 각각 할당
        deckInfoPivot = pivotTemp[0].GetComponent<DeckInfoPivot>();
        garbageFieldInfoPivot = pivotTemp[1].GetComponent<DeckInfoPivot>();

        TextManager.instance.KeywordTextPlay(this);

        deck.ShuffleDeck();
        StackInit();
    }

    public virtual void StartTurn()
    {
        #region 턴중 버프, 디버프 관리
        charactorState.StartTurnDamage(this);
        charactorState.ReductionOnStartTurn();
        charactorState.StartTurnEffect(this);
        #endregion
    }

    /// <summary>
    /// hand의 SupHand 리스트에 무작위 랜덤 드로우된 키워드 프리팹을 할당한다.
    /// </summary>
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

        // Support덱에 남아있는 키워드 프리팹 데이터를 DeckInfoPivot에게 전송
        deckInfoPivot.RecieveDeckInfo(deck.SupportDeck);
    }

    /// <summary>
    /// hand의 mainHand 리스트에 무작위 랜덤 드로우된 키워드 프리팹을 할당한다.
    /// </summary>
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

        // Main덱에 남아있는 키워드 프리팹 데이터를 DeckInfoPivot에게 전송
        deckInfoPivot.RecieveDeckInfo(deck.MainDeck);
    }

    public void GetKeywordSup(KeywordSup _keywordSup)
    {
        /*if (Resources.Load<GameObject>("Asset/Prefabs/MonsterKeywords/ToxicSlime/SupKeyword/Addicted") == _keywordSup
            && addictionStack == 0) return;*/

        // Support 키워드를 사용
        keywordSup = _keywordSup;
        TextManager.instance.SupKeywordTextPlay(this);

        hand.DisableSupHand();

        KeywordUIMovement.instance.MoveSelectedKeyword(_keywordSup);

        AddToSupGarbageField();

        Invoke("ShowKeywordMain",2);
    }

    public void GetKeywordMain(KeywordMain _keywordMain)
    {
        // Main 키워드를 사용
        keywordMain = _keywordMain;

        hand.DisableMainHand();

        KeywordUIMovement.instance.MoveSelectedKeyword(_keywordMain);

        if (_keywordMain.isOneTimeUse)
        {
            deck.DisCardByTextSource(_keywordMain.nameText);
            if(OriginalDeck != null)
                OriginalDeck.DisCardByTextSource(_keywordMain.nameText);
        }
        AddToMainGarbageField();
        TextManager.instance.MainKeywordTextPlay(this, 1f);
    }

    private void AddToSupGarbageField()
    {
        // HANDSIZE만큼 반복하여 사용한 Support 키워드 + 나머지 Support 키워드 무덤덱으로 이동
        for (int i = 0; i < hand.HANDSIZE; i++)
        {
            garbageField.AddSupKeywordOnDeck(hand.ThrowSupKeyword(0));
        }

        // 
        garbageFieldInfoPivot.RecieveDeckInfo(garbageField.SupportDeck);
    }

    private void AddToMainGarbageField()
    {
        // HANDSIZE만큼 반복하여 사용한 Main 키워드 + 나머지 Main 키워드 무덤덱으로 이동
        for (int i = 0; i < hand.HANDSIZE; i++)
        {
            garbageField.AddMainKeywordOnDeck(hand.ThrowMainKeyword(0));
        }

        // 
        garbageFieldInfoPivot.RecieveDeckInfo(garbageField.MainDeck);
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
            PlayActionEffect(target);


            target.Damaged(this, damage);

            TensionManager tensionManager = TensionManager.tensionManagerUI;
            tensionManager.tension += tension;

            // 반격 관련 코드
            if (target.attackCount == true)
            {
                int counterDamage = target.CalculateCounterAttackDamage(target);
                Damaged(this, counterDamage);
            }

            target.attackCount = false;
        }
    }

    /// <summary>
    /// 이펙트 실행하는 함수 ㅋㅋ
    /// </summary>
    protected void PlayActionEffect(Actor target)
    {
        Color mainColor = keywordMain.GetKeywordColor();
        Color supColor = keywordSup.GetKeywordColor();

        if (keywordMain.effectTarget == Keyword.EffectTarget.target)
        {
            if (mainColor == Color.red)
            {
                if (damage != 0)
                {
                    EffectManager.instance.PlayEffect(keywordMain.effectType, target);
                }
            }
            else
            {
                EffectManager.instance.PlayEffect(keywordMain.effectType, target);
            }
        }
        if (keywordMain.effectTarget == Keyword.EffectTarget.caster)
        {
            EffectManager.instance.PlayEffect(keywordMain.effectType, this);
        }

        if (keywordSup.effectTarget == Keyword.EffectTarget.target)
        {
            if (supColor == Color.red)
            {
                if (damage != 0)
                {
                    EffectManager.instance.PlayEffect(keywordSup.effectType, target);
                }
            }
            else
            {
                EffectManager.instance.PlayEffect(keywordSup.effectType, target);
            }
        }
        if (keywordSup.effectTarget == Keyword.EffectTarget.caster)
        {
            EffectManager.instance.PlayEffect(keywordSup.effectType, this);
        }
    }

    #region 공격 전 total데미지 연산
    protected int CalculateTotalDamageBeforeDamaged(int totalDamage, Actor attacker)
    {
        totalDamage = AddSubCalculationTotalDamage(totalDamage, attacker);

        totalDamage = RatioCalculateTotalDamage(totalDamage, attacker);
        return totalDamage;
    }
        #region 덧셈, 뺄셈 연산
        /// <summary>
    /// 총 데미지에 더하고 빼는 연산 실행
    /// </summary>
        protected int AddSubCalculationTotalDamage(int totalDamage, Actor attacker)
        {
            totalDamage = CalculateReinforce(totalDamage, attacker);
            totalDamage = CalculateOneTimeReinforce(totalDamage, attacker);
            totalDamage = CalculateReduction(totalDamage, attacker);
            totalDamage = CalculateWeaken(totalDamage);
            return totalDamage;
        }
        protected int CalculateReinforce(int totalDamage, Actor attacker)
        {
            totalDamage += charactorState.GetStateStack(StateType.reinforce);
            return totalDamage;
        }
        /// <summary>
        /// return totalDamage
        /// </summary>
        protected int CalculateOneTimeReinforce(int totalDamage, Actor attacker)
        {
            totalDamage += attacker.charactorState.GetStateStack(StateType.oneTimeReinforce);
            return totalDamage;
        }
        /// <summary>
        /// return totalDamage
        /// </summary>
        protected int CalculateWeaken(int totalDamage)
        {
            totalDamage += charactorState.GetStateStack(StateType.weaken);
            return totalDamage;
        }
        /// <summary>
        /// return totalDamage
        /// </summary>
        protected int CalculateReduction(int totalDamage, Actor attacker)
    {
        totalDamage -= attacker.charactorState.GetStateStack(StateType.reduction);
        return totalDamage;
    }
        #endregion
        #region 비율 연산
        protected int RatioCalculateTotalDamage(int totalDamage, Actor attacker)
        {
            totalDamage = CalculateFear(totalDamage, attacker);
            return totalDamage;
        }
        /// <summary>
        /// return totalDamage
        /// </summary>
        protected int CalculateFear(int totalDamage, Actor attacker)
        {
            int fearStack = attacker.charactorState.GetStateStack(StateType.fear);
            // 공격자 공포스택 1 당 피해량 10% 감소
            if (fearStack <= 10)
            {
                totalDamage = (int)(totalDamage * (1 - (fearStack * 0.1f)));
            }
            else
            {
                totalDamage = 0;
            }
            return totalDamage;
        }
        #endregion
    #endregion
    #region 피해 연산(보호막 등)
    protected virtual int CalculateAllProtection(int totalDamage)
    {
        totalDamage = CalculateOneTimeProtect(totalDamage);
        totalDamage = CalculateProtect(totalDamage);
        return totalDamage;
    }

    /// <summary>
    /// return totalDamage
    /// </summary>
    protected int CalculateProtect(int totalDamage)
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
        return totalDamage;
    }
    /// <summary>
    /// return totalDamage
    /// </summary>
    protected int CalculateOneTimeProtect(int totalDamage)
    {
        int oneTimeProtect = charactorState.GetStateStack(StateType.oneTimeProtect);

        if (oneTimeProtect > 0)
        {
            if (oneTimeProtect < totalDamage)
            {
                totalDamage -= oneTimeProtect;
                charactorState.ResetState(StateType.oneTimeProtect);
            }
            else
            {
                oneTimeProtect -= totalDamage;
                totalDamage = 0;
                charactorState.ResetState(StateType.oneTimeProtect);
            }
        }
        return totalDamage;
    }
    #endregion
    #region 반격 검사 및 반격 데미지 연산
    /// <summary>
    /// 반격 할건지 검사
    /// </summary>
    protected void CheckAttackCountFlag(int totalDamage, Actor attacker)
    {
        if (totalDamage > 0)
        {
            attackCount = true;
        }
    }
    protected virtual int CalculateCounterAttackDamage(Actor FightBacker)
    {
        if(!attackCount) return 0;

        int glassPragmentStack = FightBacker.charactorState.GetStateStack(StateType.glassPragment);

        int counterDamage = glassPragmentStack;
        counterDamage +=FightBacker.charactorState.GetStateStack(StateType.pike);
        
        
        return counterDamage;
    }
    #endregion

    protected void DamagedSelf(int totalDamage)
    {
        totalDamage = CalculateAllProtection(totalDamage);

        hp -= totalDamage;
    }
    protected virtual void DamagedOther(int totalDamage, Actor attacker)
    {
        // 공격 전 피해량 계산
        totalDamage = CalculateTotalDamageBeforeDamaged(totalDamage, attacker);

        // 피해량 있으면, 반격 플래그 TRUE
        CheckAttackCountFlag(totalDamage, attacker);

        // 보호막 관련 모든 연산을 실행
        totalDamage = CalculateAllProtection(totalDamage);

        hp -= totalDamage;


        // 공격자, 공격 시 스택 감소할 것들 감소
        attacker.charactorState.ReductionOnAttack();
        // 피해자, 피해 시 스택 감소할 것들 감소
        charactorState.ReductionOnDamaged();
    }
    public virtual void Damaged(Actor attacker, int _damage)
    {
        if (_damage <= 0) return;

        int totalDamage = _damage;

        if(attacker == this)
            DamagedSelf(totalDamage);
        else
            DamagedOther(totalDamage, attacker);
    }


    public void SelectKeyword()
    {
        FillSupHandInfo();
        hand.SubstantiateSupKeywordData();
    }

    private void ShowKeywordMain()
    {
        FillMainHandInfo();
        hand.SubstantiateMainKeywordData();
    }
  }
//namespace DamagedCalculate
//{
//    class DamageCalculator
//    {
//        public static void CalculateProtect(int totalDamage, int protect)
//        {
//            if (protect > 0)
//            {
//                if (protect < totalDamage)
//                {
//                    totalDamage -= protect;
//                    protect = 0;
//                }
//                else
//                {
//                    protect -= totalDamage;
//                    totalDamage = 0;
//                }
//            }

//            hp -= totalDamage;
//            return;
//        }
//    }
//}