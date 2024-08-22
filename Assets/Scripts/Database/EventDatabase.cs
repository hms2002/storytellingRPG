using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDatabase : MonoBehaviour
{
    public static EventDatabase eventDatas;
    [Header("스테이지1의 이벤트 리스트")]
    public List<EventData> stage1EventList;
    [Header("스테이지2의 이벤트 리스트")]
    public List<EventData> stage2EventList;
    [Header("스테이지3의 이벤트 리스트")]
    public List<EventData> stage3EventList;
    [Header("스테이지4의 이벤트 리스트")]
    public List<EventData> stage4EventList;
    [Header("쉼 이벤트")]
    public EventData rest;

    private void Awake()
    {
        if (eventDatas != null) Destroy(this);
        eventDatas = this;
    }

    public void ShuffleList()
    {
        int n = stage1EventList.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            EventData value = stage1EventList[k];
            stage1EventList[k] = stage1EventList[n];
            stage1EventList[n] = value;
        }
        n = stage1EventList.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            EventData value = stage2EventList[k];
            stage2EventList[k] = stage2EventList[n];
            stage2EventList[n] = value;
        }
        n = stage1EventList.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            EventData value = stage3EventList[k];
            stage3EventList[k] = stage3EventList[n];
            stage3EventList[n] = value;
        }
        n = stage4EventList.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            EventData value = stage4EventList[k];
            stage4EventList[k] = stage4EventList[n];
            stage4EventList[n] = value;
        }
    }
}
