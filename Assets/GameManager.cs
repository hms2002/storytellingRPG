using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 게임의 전체 흐름을 담당
/// <para> 현재의 게임 상태를 다른 하위 매니저들에게 전달 </para>
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("매니저")]
    [SerializeField] private FightManager fightManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private TextManager textManager;
    [SerializeField] private EffectManager effectManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private TensionManager tensionManager;
    [SerializeField] private RewardManager rewardManager;
    [SerializeField] private Texture2D cursorImg;


    /*==================================================================================================================================*/

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;

        Cursor.SetCursor(cursorImg, new Vector2(50,50), CursorMode.ForceSoftware);
    }

    public void EnterFightZone()
    {
        CallFightUIOn();
        Invoke("CallFightStart", 2);
    }
    private void CallFightUIOn()
    {
        StartCoroutine( UIManager.uIManager.EnterBattleField());
    }
    private void CallFightStart()
    {
        fightManager.FightStart();
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
        Map.MapState.InstanceMap.OnOffMap();
    }

    internal void EndSelectReward()
    {
        UIManager.uIManager.GetOutOfBattleField();
        Invoke("ReturnMap", 2);
    }
}
