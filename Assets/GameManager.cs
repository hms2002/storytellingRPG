using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 게임의 상태를 표기
/// </summary>
public enum GameState
{
    Map,
    Battle,
    KeywordSetting,
    Shop,
    Treasure
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
    [SerializeField] private Texture2D      cursorImg;

    [Header("플레이어의 모든 키워드 프리랩")]
    [SerializeField] private List<GameObject> _allSupKeywordsForPlayer;     // 플레이어가 가질 수 있는 모든 Support 키워드
    [SerializeField] private List<GameObject> _allMainKeywordsForPlayer;    // 플레이어가 가질 수 있는 모든 Main 키워드
    public IReadOnlyList<GameObject> allSupKeywordsForPlayer => _allSupKeywordsForPlayer;
    public IReadOnlyList<GameObject> allMainKeywordsForPlayer => _allMainKeywordsForPlayer;

    private GameState _gameState = GameState.Map;   // 게임의 상태를 저장
    public GameState gameState {  get => _gameState; set => _gameState = value; }


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

        Cursor.SetCursor(cursorImg, new Vector2(50,50), CursorMode.ForceSoftware);
    }

    public void EnterFightZone()
    {
        Book.instance.EnterBattleField();
        DOVirtual.DelayedCall(2.0f, fightManager.FightStart);
    }

    public void WinFight()
    {
        // 승리 UI
        // 이후 보상 UI
        rewardManager.ShowRewards();
    }

    /// <summary>
    /// 보상 선택 후, 맵으로 돌아가는 기능
    /// </summary>
    public void ReturnMap()
    {
        Book.instance.GetOutOfBattleField();
    }

    /// <summary>
    /// 상점으로 입장한다.
    /// </summary>
    public void EnterShop()
    {
        // 게임 상태를 Shop으로 전환
        gameState = GameState.Shop;

        shopManager.EnterShop();
    }
}
