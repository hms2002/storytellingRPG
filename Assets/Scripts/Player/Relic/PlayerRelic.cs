using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 플레이어가 소지하고 있는 유물들을 관리합니다.
/// </summary>
public class PlayerRelic : MonoBehaviour
{
    public static PlayerRelic instance;

    private Actor player;       // 플레이어의 Actor 컴포넌트

    [SerializeField] private List<GameObject> relics = new List<GameObject>();

    [Header("Player 유물 캔버스")]
    [SerializeField] private Transform playerRelicCanvas;


    /*==================================================================================================================================*/

    private void OnEnable()
    {
        InitRelicList();
    }
    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

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


        //if (GameManager.instance.IsInstantiated(relic))
        //{
        //    relics.Add(relic);

        //    relics[relics.Count -1].transform.SetParent(playerRelicCanvas, false);

        //    return;
        //}

        // 플레이어가 소지중인 유물의 리스트에 인스턴스화한 후 추가
        relics.Add(Instantiate(relic, playerRelicCanvas.transform));
        InitRelicList();
        GameManager.instance.relicCnt++;
    }
    public void AddRelicForInstantciated(GameObject relic)
    {
        if (relic == null) return;


        //if (GameManager.instance.IsInstantiated(relic))
        //{
        //    relics.Add(relic);

        //    relics[relics.Count -1].transform.SetParent(playerRelicCanvas, false);

        //    return;
        //}
        relic.GetComponent<Relic>().isPerchased = true;
        relic.SetActive(true);
        relic.transform.SetParent(playerRelicCanvas.transform);

        // 플레이어가 소지중인 유물의 리스트에 인스턴스화한 후 추가
        relics.Add(relic);
        InitRelicList();
        GameManager.instance.relicCnt++;
    }
    public void InitRelicList()
    {
        RelicManager.instance.RelicList.Clear();

        foreach (GameObject myRelic in relics)
        {
            RelicManager.instance.RelicList.Add(myRelic.GetComponent<Relic>().relicData);
        }
    }

    public int GetRelicCnt()
    {
        return relics.Count;
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
            if (relic.GetComponent<Relic>().relicData.relicType == relicType)
            {
                // 유물 효과 발동
                relic.GetComponent<Relic>().ApplyEffect(player, FightManager.fightManager.MonsterList);
            }
        }
    }
}
