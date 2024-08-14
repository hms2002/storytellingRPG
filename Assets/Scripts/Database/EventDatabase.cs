using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDatabase : MonoBehaviour
{
    public static EventDatabase eventDatas;
    public EventData lakeLady;

    private void OnEnable()
    {
        if (eventDatas != null) Destroy(this);
        eventDatas = this;
    }
}
