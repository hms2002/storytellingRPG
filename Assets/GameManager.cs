using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 게임의 상태를 표기
/// </summary>
public enum GameState
{
    Map,
    Battle,
    EndBattle,
    BossBattle,
    KeywordSetting,
    Shop,
    Rest,
    Treasure,
    Event,
    Title,
    Ending,
    GameOver
}


/// <summary>
/// 게임의 전체 흐름을 담당
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("매니저")]
    [SerializeField] private FightManager   fightManager;
    [SerializeField] private UIManager      uiManager;
    [SerializeField] private TextManager    textManager;
    [SerializeField] private EffectManager  effectManager;
    [SerializeField] private AudioManager   audioManager;
    [SerializeField] private TensionManager tensionManager;
    [SerializeField] private RewardManager  rewardManager;
    [SerializeField] private ShopManager    shopManager;
    [SerializeField] private RestManager    restManager;
    [SerializeField] private EventManager   eventManager;

    [Header("플레이어의 모든 키워드 프리랩")]
    [SerializeField] private List<GameObject> _allSupKeywordsForPlayer;     // 플레이어가 가질 수 있는 모든 Support 키워드
    [SerializeField] private List<GameObject> _allMainKeywordsForPlayer;    // 플레이어가 가질 수 있는 모든 Main 키워드
    public IReadOnlyList<GameObject> allSupKeywordsForPlayer => _allSupKeywordsForPlayer;
    public IReadOnlyList<GameObject> allMainKeywordsForPlayer => _allMainKeywordsForPlayer;

    private GameState _gameState = GameState.Map;   // 게임의 상태를 저장
    public GameState gameState {  get => _gameState; set => _gameState = value; }

    int killCnt_nomalMonster = 0;
    int killCnt_eleteMonster = 0;
    int killCnt_bossMonster = 0;
    public int relicCnt = 0;
    public int goldCnt = 0;
    public int keywordCnt = 0;


    public int eventIndex = 0;

    /*==================================================================================================================================*/


    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        if(uiManager !=null)
            uiManager.ChangeCursorImage(CursorType.Nib);
    }

    private void Start()
    {
        //EventDatabase.eventDatas.ShuffleList();
    }

    Map.NodeType monsterType;
    //전투 돌입 (일반 몹, 보스 몹)
    public void EnterFightZone(Map.NodeType type)
    {
        monsterType = type;
        Book.instance.EnterBattleField();
        DOVirtual.DelayedCall(2.0f, fightManager.FightStart);
    }
    
    public void WinFight()
    {
        // 승리 UI
        // 이후 보상 UI
        rewardManager.ShowFightRewards_Keyword();
        switch(monsterType)
        {
            case Map.NodeType.BossNode:
                killCnt_bossMonster++;
                break;
            case Map.NodeType.EliteMonsterNode:
                killCnt_eleteMonster++;
                break;
            case Map.NodeType.NomalMonsterNode:
                killCnt_nomalMonster++;
                    break;
        }
    }

    /// <summary>
    /// 보상 선택 후, 맵으로 돌아가는 기능
    /// </summary>
    public void ReturnMap()
    {
        if(FightManager.fightManager.isBossStage)
        {
            StageManager.instance.NextStage();
            FightManager.fightManager.isBossStage = false;
        }
        else
        {
            Book.instance.GetOutOfBattleField();
        }
    }

    /// <summary>
    /// 상점으로 입장한다.
    /// </summary>
    public void EnterShop()
    {
        // 게임 상태를 Shop으로 전환
        gameState = GameState.Shop;

        shopManager.EnterShop();
        Book.instance.bookAnimator.SetTrigger("turnPageToRight");
    }

    public void EnterRest()
    {
        gameState = GameState.Rest;
        Book.instance.EnterRestField();
    }

    public void EnterEvent()
    {
        gameState = GameState.Event;
        Book.instance.EnterEventField();
    }
    public void EnterTreasure()
    {
        gameState = GameState.Event;
        Book.instance.EnterTreasureField();
    }

    public void PrintGameClearCredit()
    {
        RecordTextDatabase recordDatas = RecordTextDatabase.instance;
        recordDatas.timeText.text                   = recordDatas.timeText.text + " " + Time.time / 60 + "분 " + Time.time % 60 + "초";
        recordDatas.nomalMonsterCounting.text       = recordDatas.nomalMonsterCounting.text + " " + killCnt_nomalMonster.ToString();
        recordDatas.eliteMonsterCounting.text       = recordDatas.eliteMonsterCounting.text + " " + killCnt_eleteMonster.ToString();
        recordDatas.bossCounting.text               = recordDatas.bossCounting.text + " " + killCnt_bossMonster.ToString();
        recordDatas.GetRelicCounting.text           = recordDatas.GetRelicCounting.text + " " + relicCnt.ToString();
        recordDatas.GetGoldCounting.text            = recordDatas.GetGoldCounting.text + " " + goldCnt.ToString();
        recordDatas.GetKeywordCounting.text         = recordDatas.GetKeywordCounting.text + " " + keywordCnt.ToString();
    }
    internal void EndSelectReward()
    {
        ReturnMap();
    }

    public void LoadScene(int idx)
    {
        SceneManager.LoadScene(0);
    }
}
