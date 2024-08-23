using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static RelicData;

public class RelicManager : MonoBehaviour
{
    public static RelicManager instance;

    [Header("유물 프리팹")]
    [SerializeField] private GameObject relicPrefab;
    
    [SerializeField] private List<RelicData> relicList = new List<RelicData>();
    public List<RelicData> RelicList => relicList;

    private int randomIndex;        // 무작위 유물 리스트 인덱스를 담을 변수


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
    }

    /// <summary>
    /// 유물들 중 하나를 무작위로 뽑아줍니다.
    /// </summary>
    /// <returns>뽑은 유물 데이터를 프리팹에 적용하여 유물 프리팹을 반환합니다.</returns>
    public GameObject GetRandomRelic()
    {
        bool overlapFlag = true;

        while (overlapFlag)
        {
            overlapFlag = false;

            // 유물 리스트 중 하나의 인덱스를 무작위로 선택
            randomIndex = Random.Range(0, RelicDatabase.instance.relicsData.Count);

            foreach (RelicData relic in relicList)
            {
                if (relic == RelicDatabase.instance.relicsData[randomIndex] || relic.GetComponent<RelicData>().relicAcquirePath != RelicAcquirePath.Common)
                {
                    overlapFlag = true;

                    break;
                }
            }
        }

        relicList.Add(RelicDatabase.instance.relicsData[randomIndex]);

        // 무작위 선택된 유물을 유물 프리팹에 적용
        relicPrefab.GetComponent<Relic>().relicData = RelicDatabase.instance.relicsData[randomIndex];

        return relicPrefab;
    }

    public void GetRelic(Relics whatRelic)
    {
        relicPrefab.GetComponent<Relic>().relicData = RelicDatabase.instance.relicsData[(int)whatRelic];

        PlayerRelic.instance.AddRelic(relicPrefab);
    }

    public void AddRelicWithData(RelicData relicData)
    {
        relicPrefab.GetComponent<Relic>().relicData = relicData;

        PlayerRelic.instance.AddRelic(relicPrefab);
    }

    public int HowManyRelicLeft()
    {
        return RelicDatabase.instance.relicsData.Count - PlayerRelic.instance.GetRelicCnt();
    }
}
