using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }

    private RewardManager rewardManager;
    private FightManager fightManager;



    void Start()
    {
        rewardManager = FindAnyObjectByType<RewardManager>();
        fightManager = FindAnyObjectByType<FightManager>();
    }

    public void EnterFightZone()
    {
        CallFightUIOn();
        Invoke("CallFightStart", 1);
    }
    private void CallFightUIOn()
    {
        UIManager.uIManager.EnterBattleField();
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

    }

    internal void EndSelectReward()
    {
        // 지금은 다음 몬스터 데려오는 코드로 짜야 함. - 프로토타입 전용 코드
        UIManager.uIManager.GetOutOfBattleField();
        Invoke("EnterFightZone", 2);
    }
}
