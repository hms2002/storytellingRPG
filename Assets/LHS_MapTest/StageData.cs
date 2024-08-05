using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageState
{
    Forest,
    Cave,
    Sea
}

public class StageData : ScriptableObject
{
    int nowStage = 0;
}

public class MapBackGround
{
    public GameObject[] mapBackGrounds = new GameObject[3];

    public void BackgroundSetting(int nowStage)
    {
        for (int i = 0; i < mapBackGrounds.Length; i++)
        {
            mapBackGrounds[i].SetActive(false);
        }

        if(nowStage >= 0 && nowStage <= mapBackGrounds.Length)
        {
            mapBackGrounds[nowStage].SetActive(true);
        }
        else
        {
            Debug.LogWarning("범위를 벗어난 스테이지");
        }
    }
}