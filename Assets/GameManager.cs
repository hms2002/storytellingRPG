using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    [SerializeField] private FightManager   _fightManager;
    private FightManager fightManager 
    { 
        get 
        { 
            if(_fightManager == null)
            {
                _fightManager = FightManager.fightManager;
                if (_fightManager == null)
                {
                    _fightManager = FindObjectOfType<FightManager>();
                    if (_fightManager == null)
                        Debug.LogWarning("매니저 없음. 참조하지 마..");
                }
            }
            return _fightManager;
                
        } 
    }
    [SerializeField] private UIManager      _uiManager;
    private UIManager uiManager
    {
        get
        {
            if (_uiManager == null)
            {
                _uiManager = UIManager.instance;
                if (_uiManager == null)
                {
                    _uiManager = FindObjectOfType<UIManager>();
                    if (_uiManager == null)
                        Debug.LogWarning("매니저 없음. 참조하지 마..");
                }
            }
            return _uiManager;
        }
    }
    [SerializeField] private TextManager    _textManager;
    private TextManager textManager
    {
        get
        {
            if (_textManager == null)
            {
                _textManager = TextManager.instance;
                if (_textManager == null)
                {
                    _textManager = FindObjectOfType<TextManager>();
                    if (_textManager == null)
                        Debug.LogWarning("매니저 없음. 참조하지 마..");
                }
            }
            return _textManager;
        }
    }
    [SerializeField] private EffectManager  _effectManager;
    private EffectManager effectManager
    {
        get
        {
            if (_effectManager == null)
            {
                _effectManager = FindObjectOfType<EffectManager>();
                if (_effectManager == null)
                    Debug.LogWarning("매니저 없음. 참조하지 마..");
            }
            return _effectManager;
        }
    }
    [SerializeField] private AudioManager   _audioManager;
    private AudioManager audioManager
    {
        get
        {
            if (_audioManager == null)
            {
                _audioManager = AudioManager.instance;
                if (_audioManager == null)
                {
                    _audioManager = FindObjectOfType<AudioManager>();
                    if (_audioManager == null)
                        Debug.LogWarning("매니저 없음. 참조하지 마..");
                }
            }
            return _audioManager;
        }
    }
    [SerializeField] private TensionManager _tensionManager;
    private TensionManager tensionManager
    {
        get
        {
            if (_tensionManager == null)
            {
                _tensionManager = TensionManager.tensionManagerUI;
                if (_tensionManager == null)
                {
                    _tensionManager = FindObjectOfType<TensionManager>();
                    if (_tensionManager == null)
                        Debug.LogWarning("매니저 없음. 참조하지 마..");
                }
            }
            return _tensionManager;
        }
    }
    [SerializeField] private RewardManager  _rewardManager;
    private RewardManager rewardManager
    {
        get
        {
            if (_rewardManager == null)
            {
                _rewardManager = RewardManager.instance;
                if (_rewardManager == null)
                {
                    _rewardManager = FindObjectOfType<RewardManager>();
                    if (_rewardManager == null)
                        Debug.LogWarning("매니저 없음. 참조하지 마..");
                }
            }
            return _rewardManager;
        }
    }
    [SerializeField] private ShopManager    _shopManager;
    private ShopManager shopManager
    {
        get
        {
            if (_shopManager == null)
            {
                _shopManager = ShopManager.instance;
                if (_shopManager == null)
                {
                    _shopManager = FindObjectOfType<ShopManager>();
                    if (_shopManager == null)
                        Debug.LogWarning("매니저 없음. 참조하지 마..");
                }
            }
            return _shopManager;
        }
    }
    [SerializeField] private RestManager    _restManager;
    private RestManager restManager
    {
        get
        {
            if (_restManager == null)
            {
                _restManager = FindObjectOfType<RestManager>();
                if (_restManager == null)
                    Debug.LogWarning("매니저 없음. 참조하지 마..");
            }
            
            return _restManager;
        }
    }
    [SerializeField] private EventManager   _eventManager;
    private EventManager eventManager
    {
        get
        {
            if (_eventManager == null)
            {
                _eventManager = EventManager.instance;
                if (_eventManager == null)
                {
                    _eventManager = FindObjectOfType<EventManager>();
                    if (_eventManager == null)
                        Debug.LogWarning("매니저 없음. 참조하지 마..");
                }
            }
            return _eventManager;
        }
    }

    [Header("플레이어의 모든 키워드 프리랩")]
    [SerializeField] private List<GameObject> _allSupKeywordsForPlayer;     // 플레이어가 가질 수 있는 모든 Support 키워드
    [SerializeField] private List<GameObject> _allMainKeywordsForPlayer;    // 플레이어가 가질 수 있는 모든 Main 키워드
    public IReadOnlyList<GameObject> allSupKeywordsForPlayer => _allSupKeywordsForPlayer;
    public IReadOnlyList<GameObject> allMainKeywordsForPlayer => _allMainKeywordsForPlayer;

    public GameState _gameState = GameState.Map;   // 게임의 상태를 저장
    private GameState beforeState;
    public GameState gameState 
    {  get => _gameState;
        set 
        {
            _gameState = beforeState;
            _gameState = value;
            if(beforeState == _gameState)
            {
                return;
            }
            else 
            {
                AudioManager.instance.UpdateBGM();
            }
        }
    }

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
        gameState = GameState.Map;
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
        switch(monsterType)
        {
            case Map.NodeType.BossNode:
                rewardManager.dropRelic = true;
                killCnt_bossMonster++;
                break;
            case Map.NodeType.EliteMonsterNode:
                rewardManager.dropRelic = true;
                killCnt_eleteMonster++;
                break;
            case Map.NodeType.NomalMonsterNode:
                killCnt_nomalMonster++;
                    break;
        }
        rewardManager.ShowFightRewards_Keyword();
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
    [SerializeField] GameObject gameOverCanvas;
    public void GameOver()
    {
        DOVirtual.DelayedCall(2.0f, () => GameOverCanvasDatabase.instance.GameOverCanvas.SetActive(true));
        DOVirtual.DelayedCall(5.0f, () => LoadScene(0));
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
        SceneManager.LoadScene(idx);
    }
}
