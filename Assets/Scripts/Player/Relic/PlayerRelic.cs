using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 소지하고 있는 유물들을 관리합니다.
/// </summary>
public class PlayerRelic : MonoBehaviour
{
    private Actor player;

    private List<GameObject> relics = new List<GameObject>();

    [Header("Player 유물 캔버스")]
    [SerializeField] private GameObject playerRelicCanvas;

    /*==================================================================================================================================*/


    private void Start()
    {
        player = GetComponent<Actor>();
    }

    /// <summary>
    /// 유물 리스트에 유물을 인스턴스화한 후 추가합니다.
    /// </summary>
    /// <param name="relic">플레이어에게 추가하고자 하는 유물 프리팹을 전달합니다.</param>
    public void AddRelic(GameObject relic)
    {
        if (relic == null) return;

        // 플레이어가 소지중인 유물의 리스트에 인스턴스화한 후 추가
        relics.Add(Instantiate(relic, playerRelicCanvas.transform));
    }

    /// <summary>
    /// 전달받은 타입에 맞는 유물들을 찾아 효과를 적용시킵니다.
    /// </summary>
    /// <param name="relicType"></param>
    public void UseRelic(RelicData.RelicType relicType)
    {
        foreach (GameObject relic in relics)
        {
            // 유물의 발동 타입이 일치한다면
            if (relic.GetComponent<RelicData>().relicType == relicType)
            {
                // 유물 효과 발동
                relic.GetComponent<Relic>().ApplyEffect(player, FightManager.fightManager.MonsterList);
            }
        }
    }
}
