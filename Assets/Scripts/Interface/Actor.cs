using DG.Tweening;
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

public class DamageInfo
    {
        public int damage;
        public bool isPenetrate;
        public DamageInfo(int _damage, bool _isPenetrate)
        {
            damage = _damage;
            isPenetrate = _isPenetrate;
        }
        public DamageInfo(int _damage)
        {
            damage = _damage;
            isPenetrate = false;
        }
    }
public class DamageList
{
    public List<DamageInfo> damageL = new List<DamageInfo>();
    bool isPlus = false;
    public int GetAllDamage()
    {
        int damage = 0;
        foreach (DamageInfo d in damageL)
            damage += d.damage;
        return damage;
    }
    public void Add(int damage, bool isPenetrate = false)
    {
        if(isPlus)
        {
            isPlus = false;
            if(damageL.Count == 0)
                damageL.Add(new DamageInfo(damage, isPenetrate));
            else if(damageL[0] != null)
                damageL[0].damage += damage;
            else
                damageL.Add(new DamageInfo(damage, isPenetrate));
        }
        else
            damageL.Add(new DamageInfo(damage, isPenetrate));
    }
    /// <summary>
    /// 서폿 키워드가 데미지 입히면 이거임
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="isPenetrate"></param>
    public void Plus(int damage, bool isPenetrate = false)
    {
        if(damageL.Count == 0)
        {
            isPlus = true;
            damageL.Add(new DamageInfo(damage, isPenetrate));
        }
        else
        {
            if (damageL[0] == null)
                damageL.Add(new DamageInfo(damage, isPenetrate));
            else
                damageL[0].damage += damage;
        }
    }
}
public class Actor : MonoBehaviour
{

    /// <summary>
    /// 데미지 모아주는 컨테이너
    /// </summary>
    public DamageList dmgList = new DamageList();

    [Header("상태창 UI")]
    public ActorStateUIControler stateUIController;
    public readonly CharactorState charactorState = new CharactorState();

    public string _attackSound = "타격음_주먹2";


    public void AddSupKeywordToOriginalDeck(GameObject keywordSup)
    {
        GameManager.instance.keywordCnt++;
        originalDeck.AddSupKeywordOnDeck(keywordSup);
    }
    public void AddMainKeywordToOriginalDeck(GameObject keywordMain)
    {
        GameManager.instance.keywordCnt++;
        originalDeck.AddMainKeywordOnDeck(keywordMain);
    }

    #region Actor의 키워드 관련 변수
    private   Deck originalDeck;                    // 
    protected Deck deck;                            // Actor가 갖고 있는 "기본"덱 (Support, Main 키워드)
    protected Hand hand;                            // Actor의 손패 (Support, Main 키워드)
    protected Deck garbageField = new Deck();       // Actor가 갖고 있는 "무덤"덱 (Support, Main 키워드)

    public Deck OriginalDeck => originalDeck;

    [Header("덱 정보 피봇")]
    protected DeckInfoPivot deckInfoPivot;          // 전투 중 기본덱을 확인
    protected DeckInfoPivot garbageFieldInfoPivot;  // 전투 중 무덤덱을 확인

    private KeywordSup _keywordSup;
    private KeywordMain _keywordMain;

    public KeywordSup keywordSup { get => _keywordSup; set => _keywordSup = value; }
    public KeywordMain keywordMain { get => _keywordMain; set => _keywordMain = value; }
    #endregion

    #region Actor의 능력치 관련 변수, 함수
    private int _tension = 0;

    [Header("최대 체력")]
    [SerializeField] protected int _MAX_HP = 100;
    [SerializeField] private int _hp;
    private int _protect = 0;
    private int _lastTurnProtectReduction = 0;
    private int _lastProtectReductTerminal= 0;
    private int _heal = 0;
    private int _damage = 0;
    private int _repeatStack = 1;
    private int _additionalDamage = 0;
    private int _additionalStack = 0;
    private bool _attackCount = false;
    [SerializeField]
    [Header("이름")]
    private string _Name;

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
            if (charactorState != null)
                if (charactorState.GetStateStack(StateType.ore) != 0 && value < _hp)
                    charactorState.ReductionByValue(StateType.ore, _hp - value);

            if(value > _hp && value != MAX_HP)
                UIManager.instance.ActiveDamageText(transform.position, value-_hp, Color.green);


            _hp = value;
            if (_hp > _MAX_HP)
            {
                _hp = _MAX_HP;
            }
            if (_hp < 0)
            {
                _hp = 0;
            }
            if (stateUIController != null)
                stateUIController.UpdateHpUI(_hp, MAX_HP);
        }
    }

    public int protect
    {
        get { return _protect; }
        set
        {
            if (_protect > value)
                _lastProtectReductTerminal += _protect - value;
            if (value > _protect)
            {
                AudioManager.instance.PlaySound("Character", "보호");
            }
            _protect = value;
            if (_protect < 0) _protect = 0;
            stateUIController.ProtectOn(_protect);
        }
    }
    
    public int lastTurnProtectReduction
    {
        get { return _lastTurnProtectReduction; }
        set { _lastTurnProtectReduction = value; }
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

    public string Name { get => _Name; set => _Name = value; }

    #endregion

    #region 플레이어 델리게이트
    public delegate void AfterAttackDel(Actor caster, Actor target);
    public  AfterAttackDel afterAttackDel;
    #endregion

    public int beforeDamage = 0;      // 플레이어의 최근 준 데미지 계산

    [SerializeField] private int _gold = 0; // 플레이어 소지금
    public int gold
    {
        get { return _gold; }
        set
        {
            if (value < 0) value = 0;

            if (_gold < value && gameObject.tag == "Player")
                GameManager.instance.goldCnt += value - _gold;

            _gold = value;
        }
    }

    /*==================================================================================================================================*/


    bool isFirstTime = true;
    private void OnEnable()
    {
        if(isFirstTime == true)
        {
            _hp = _MAX_HP;
            isFirstTime = false;
            if (gameObject.tag == "Player")
                gold = 150;
            else if (gameObject.tag == "Elete")
                gold = UnityEngine.Random.Range(80, 100);
            else if (gameObject.tag == "Boss")
                gold = 200;
            else
                gold = UnityEngine.Random.Range(40, 61);
            hp = MAX_HP;
        }

        // 원본 덱 가져오기 전에 있는지 확인
        if ((int)transform.childCount >= 2)
            originalDeck = transform.GetChild(1).GetComponent<Deck>();

        deck = GetComponent<Deck>();
        hand = GetComponent<Hand>();
        // 원본 덱 없으면 복사 X
        if (originalDeck != null)
        {
            Debug.Log(originalDeck.gameObject.name);
            deck.InitDeck(originalDeck);
        }

        garbageField.InitDeck();

        charactorState.Init(stateUIController);
        charactorState.actor = this;

        _protect = 0;
        stateUIController.ProtectOn(_protect);
        _heal = 0;
        _damage = 0;
        _repeatStack = 1;
        _additionalDamage = 0;
        _additionalStack = 0;
        _attackCount = false;
        stateUIController.UpdateHpUI(_hp, MAX_HP);
    }

    private void StackInit()
    {
        tension = 0;
        heal = 0;
        damage = 0;
        tension = 0;
        repeatStack = 1;
    }

    public virtual void BeforeFightStart(Actor target)
    {

    }

    public virtual void BeforeAction()
    {
        // 씬에 존재하는 DeckPivot 태그의 오브젝트 찾기
        GameObject[] pivotTemp = GameObject.FindGameObjectsWithTag("DeckPivot");

        // 찾은 DeckPivot 오브젝트 각각 할당
        deckInfoPivot = pivotTemp[0].GetComponent<DeckInfoPivot>();
        garbageFieldInfoPivot = pivotTemp[1].GetComponent<DeckInfoPivot>();


        deck.ShuffleDeck();
        StackInit();


        #region 턴중 버프, 디버프 관리
        charactorState.StartTurnDamage(this);
        charactorState.ReductionOnStartTurn();
        charactorState.StartTurnEffect(this);
        if (charactorState.allStateList[(int)StateType.callingOfMommyDragon] != null
            && charactorState.GetStateStack(StateType.callingOfMommyDragon) == 0)
        {
            FightManager.fightManager.MonsterFlee(this);
        }
        if (charactorState.allStateList[(int)StateType.secession] != null
    && charactorState.GetStateStack(StateType.secession) == 0)
        {
            FightManager.fightManager.MonsterFlee(this);
        }
        #endregion
    }

    /// <summary>
    /// 턴을 시작하기 전에 버프, 디버프 관리를 총괄한다.
    /// </summary>
    public virtual void StartTurn()
    {
        _lastTurnProtectReduction = _lastProtectReductTerminal;
        _lastProtectReductTerminal = 0;

        dmgList.damageL.Clear();

    }

    /// <summary>
    /// hand의 SupHand 리스트에 무작위 랜덤 드로우된 키워드 프리팹을 할당한다.
    /// </summary>
    protected virtual void FillSupHandInfo()
    {
        // Keyword 드로우 3번 반복
        for (int i = 0; i < hand.HANDSIZE; i++)
        {
            // Support덱이 비어있다면
            if (deck.IsSupDeckEmpty())
            {
                // 무덤덱에서 카드를 꺼내와 Support덱을 초기화
                for (int j = 0; j < garbageField.SupportDeck.Count; j++)
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
    protected virtual void FillMainHandInfo()
    {
        // Keyword 드로우 3번 반복
        for (int i = 0; i < hand.HANDSIZE; i++)
        {
            // Main덱이 비어있다면
            if (deck.IsMainDeckEmpty())
            {
                // 무덤덱에서 카드를 꺼내와 Main덱을 초기화
                for (int j = 0; j < garbageField.MainDeck.Count; j++)
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
        // Support 키워드를 사용
        keywordSup = _keywordSup;
        TextManager.instance.SupKeywordTextPlay(this);

        hand.DisableSupHand();

        deckInfoPivot.arePlayerChoosingKeyword          = false;
        garbageFieldInfoPivot.arePlayerChoosingKeyword  = false;

        KeywordUIMovement.instance.MoveSelectedKeyword(_keywordSup);

        AddToSupGarbageField();

        // 2초 뒤 Main 키워드
        DOVirtual.DelayedCall(1.0f, ShowMainKeywords);
    }

    public void GetKeywordMain(KeywordMain _keywordMain)
    {
        // Main 키워드를 사용
        keywordMain = _keywordMain;

        hand.DisableMainHand();

        deckInfoPivot.arePlayerChoosingKeyword          = false;
        garbageFieldInfoPivot.arePlayerChoosingKeyword  = false;

        KeywordUIMovement.instance.MoveSelectedKeyword(_keywordMain);

        if (_keywordMain.isOneTimeUse)
        {
            deck.DisCardByTextSource(_keywordMain.gameObject.name);
            hand.DisCardByTextSource(_keywordMain.gameObject.name);
            if(originalDeck != null)
                originalDeck.DisCardByTextSource(_keywordMain.gameObject.name);
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
    }

    private void AddToMainGarbageField()
    {
        int handSize = hand.mainHandSize;
        // HANDSIZE만큼 반복하여 사용한 Main 키워드 + 나머지 Main 키워드 무덤덱으로 이동
        for (int i = 0; i < handSize; i++)
        {
            garbageField.AddMainKeywordOnDeck(hand.ThrowMainKeyword(0));
        }
    }

    public virtual void Action(Actor target)
    {
        charactorState.ReductionOnBeforeAttack();
        keywordSup.Check(keywordMain);
        keywordMain.Check(keywordSup);

        keywordSup.Execute(this, target);
        keywordMain.Execute(this, target);
        Execute(target);

        if (afterAttackDel != null)
            afterAttackDel(this, target);
        afterAttackDel = null;
    }

    public void Execute(Actor target)
    {
        for (int i = 1; i <= repeatStack; i++)
        {
            PlayActionEffect(target);

            foreach(DamageInfo t in dmgList.damageL)
            {
                target.Damaged(this, t);
                beforeDamage = damage;

                // 반격 관련 코드
                if (target.attackCount == true)
                {
                    int counterDamage = target.CalculateCounterAttackDamage(target);
                    Damaged(this, new DamageInfo(counterDamage, false));
                }

                target.attackCount = false;
            }
        }
        TensionManager tensionManager = TensionManager.tensionManagerUI;
        tensionManager.tension += tension;
    }

    /// <summary>
    /// 이펙트 실행하는 함수 ㅋㅋ
    /// </summary>
    protected void PlayActionEffect(Actor target)
    {
        Color mainColor = keywordMain.GetKeywordColor();
        Color supColor = keywordSup.GetKeywordColor();
        if (!keywordMain.isIrregularCombo)
        {
            if (keywordMain.effectTarget == Keyword.EffectTarget.target)
            {
                if (mainColor == Color.red)
                {
                    if (dmgList.GetAllDamage() != 0)
                    {
                        if (dmgList.damageL.Count > 1 || repeatStack > 1)
                        {
                            /*EffectManager.instance.PlayEffect(keywordMain.effectType, target, repeatStack);*/
                            EffectManager.instance.StartPlayEffectWithDelay(EffectManager.EffectType.Combo, target, dmgList.damageL.Count);
                        }
                        else
                        {
                            EffectManager.instance.PlayEffect(keywordMain.effectType, target);
                        }
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
        }

        if (keywordSup.effectTarget == Keyword.EffectTarget.target)
        {
            if (supColor == Color.red)
            {
                if (dmgList.GetAllDamage() != 0)
                {
                    if (dmgList.damageL.Count > 1 || repeatStack > 1)
                    {
                        if(mainColor != Color.red)
                        {
                            /*EffectManager.instance.PlayEffect(keywordMain.effectType, target, repeatStack);*/
                            EffectManager.instance.StartPlayEffectWithDelay(EffectManager.EffectType.Combo, target, dmgList.damageL.Count);
                        }
                    }
                    else
                    {
                        EffectManager.instance.PlayEffect(keywordSup.effectType, target);
                    }
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
        totalDamage += attacker.charactorState.GetStateStack(StateType.reinforce);
        return totalDamage;
    }
    /// <summary>
    /// return totalDamage
    /// </summary>
    protected int CalculateOneTimeReinforce(int totalDamage, Actor attacker)
    {
        if(attacker.charactorState.allStateList[(int)StateType.oneTimeReinforce] != null && attacker.charactorState.allStateList[(int)StateType.oneTimeReinforce].gainDoubleStack)
        {
            totalDamage += attacker.charactorState.GetStateStack(StateType.oneTimeReinforce);
            attacker.charactorState.allStateList[(int)StateType.oneTimeReinforce].gainDoubleStack = false;
        }
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
    protected virtual int CalculateProtect(int totalDamage)
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
        counterDamage += FightBacker.charactorState.GetStateStack(StateType.pike);
        counterDamage += FightBacker.charactorState.GetStateStack(StateType.counterAttack);
        FightBacker.charactorState.ReductionByValue(StateType.counterAttack, 1);

        return counterDamage;
    }
    #endregion

    protected void DamagedSelf(int totalDamage)
    {
        totalDamage = CalculateAllProtection(totalDamage);
        if (totalDamage < 0) totalDamage = 0;
        UIManager.instance.ActiveDamageText(transform.position, totalDamage, Color.black);
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
        if (totalDamage <= 0)
        {
            if(dmgList.damageL.Count == 1)   
                AudioManager.instance.PlaySound("Character", attackSound);
            totalDamage = 0;
        }
        UIManager.instance.ActiveDamageText(transform.position, totalDamage, Color.red);

        hp -= totalDamage;

        // 공격자, 공격 시 스택 감소할 것들 감소
        attacker.charactorState.ReductionOnAttack();
        // 피해자, 피해 시 스택 감소할 것들 감소
        charactorState.ReductionOnDamaged();
    }
    /// <summary>
    /// 관통 데미지를 받는 함수
    /// </summary>
    /// <param name="totalDamage"></param>
    /// <param name="attacker"></param>

    public virtual void PenetrateDamaged(int totalDamage, Actor attacker)
    {
        // 공격 전 피해량 계산
        totalDamage = CalculateTotalDamageBeforeDamaged(totalDamage, attacker);

        // 피해량 있으면, 반격 플래그 TRUE
        CheckAttackCountFlag(totalDamage, attacker);

        // 보호막 관련 모든 연산을 무시
        //totalDamage = CalculateAllProtection(totalDamage);

        if (totalDamage < 0) totalDamage = 0;
        UIManager.instance.ActiveDamageText(transform.position, totalDamage, Color.red);

        hp -= totalDamage;

        // 공격자, 공격 시 스택 감소할 것들 감소
        attacker.charactorState.ReductionOnAttack();
        // 피해자, 피해 시 스택 감소할 것들 감소
        charactorState.ReductionOnDamaged();
    }

    public virtual void Damaged(Actor attacker, DamageInfo _damage)
    {
        if (_damage.damage <= 0) return;

        if (charactorState.GetStateStack(StateType.evasion) > 0)
        {
            int success = UnityEngine.Random.Range(0, 2);
            if (success == 0)
            {
                UIManager.instance.ActiveDamageText(transform.position, "회피", Color.gray);

                return;
            }
        }
        int totalDamage = _damage.damage;

        if (attacker == this)
            DamagedSelf(totalDamage);
        else
        {
            if (_damage.isPenetrate)
                PenetrateDamaged(_damage.damage, attacker);
            else
                DamagedOther(totalDamage, attacker);
        }
    }


    /// <summary>
    /// 도트데미지를 처리하는 Damaged오버로딩이다.
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="_damage"></param>
    /// <param name="_stack"></param>
    public virtual void Damaged(Actor attacker, int _damage, int _stack)
    {
        if (_damage <= 0) return;

        int totalDamage = _damage * _stack;

        if (attacker == this)
            DamagedSelf(totalDamage);
        else
            DamagedOther(totalDamage, attacker);
    }

    public virtual void ShowSupKeywords()
    {
        // 덱과 무덤덱 정보 리스트 초기화
        deckInfoPivot.ClearDeckInfo();
        garbageFieldInfoPivot.ClearDeckInfo();

        FillSupHandInfo();
        hand.SubstantiateSupKeywordData();

        // 플레이어가 키워드 선택중이라는 데이터를 정보 피봇들에게 전달
        deckInfoPivot.arePlayerChoosingKeyword = true;
        garbageFieldInfoPivot.arePlayerChoosingKeyword = true;

        // 무덤덱 정보 리스트에 버려진 Support 키워드 프리팹 정보 전달
        garbageFieldInfoPivot.RecieveDeckInfo(garbageField.SupportDeck);
    }

    protected virtual void ShowMainKeywords()
    {
        deckInfoPivot.ClearDeckInfo();
        garbageFieldInfoPivot.ClearDeckInfo();

        FillMainHandInfo();
        hand.SubstantiateMainKeywordData();

        deckInfoPivot.arePlayerChoosingKeyword = true;
        garbageFieldInfoPivot.arePlayerChoosingKeyword = true;

        // 무덤덱 정보 리스트에 버려진 Main 키워드 프리팹 정보 전달
        garbageFieldInfoPivot.RecieveDeckInfo(garbageField.MainDeck);
    }
}