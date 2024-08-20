using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 소지하고 있는 유물들을 관리합니다.
/// </summary>
public class PlayerRelic : MonoBehaviour
{
    private Actor player;

    private List<Relic> relics = new List<Relic>();


    /*==================================================================================================================================*/


    private void Start()
    {
        player = GetComponent<Actor>();
    }

    /// <summary>
    /// 유물 리스트에 유물을 추가합니다.
    /// </summary>
    /// <param name="relic"></param>
    public void AddRelic(Relic relic)
    {
        if (relic == null) return;

        relics.Add(relic);
    }

    /// <summary>
    /// 전투 시작 시 발동되는 타입의 유물들을 찾아 효과를 적용시킵니다.
    /// </summary>
    public void UseOnStartBattleRelic()
    {
        foreach (Relic relic in relics)
        {
            // 유물의 발동 타입이 OnStartBattle이면
            if (relic.RelicData.relicType == RelicData.RelicType.OnStartBattle)
            {
                // 유물 효과 발동
                relic.ApplyEffect(player, FightManager.fightManager.MonsterList);
            }
        }
    }

    /// <summary>
    /// 전투 승리 시 발동되는 타입의 유물들을 찾아 효과를 적용시킵니다.
    /// </summary>
    public void UseOnOnVictoryRelic()
    {
        foreach (Relic relic in relics)
        {
            // 유물의 발동 타입이 OnEndBattle이면
            if (relic.RelicData.relicType == RelicData.RelicType.OnVictory)
            {
                // 유물 효과 발동
                relic.ApplyEffect(player, FightManager.fightManager.MonsterList);
            }
        }
    }

    /// <summary>
    /// 턴 시작 시 발동되는 타입의 유물들을 찾아 효과를 적용시킵니다.
    /// </summary>
    public void UseOnStartTurnRelic()
    {
        foreach (Relic relic in relics)
        {
            // 유물의 발동 타입이 OnStartTurn이면
            if (relic.RelicData.relicType == RelicData.RelicType.OnStartTurn)
            {
                // 유물 효과 발동
                relic.ApplyEffect(player, FightManager.fightManager.MonsterList);
            }
        }
    }
}
