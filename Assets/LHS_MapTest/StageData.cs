using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Map;


public enum StageState
{
    Forest,
    Cave,
    Sea,
    MagicTower
}

public class StageData : MonoBehaviour
{
    public static StageData instance;

    [Header("0:숲 │ 1:동굴 │ 2: 바다 │ 3: 마탑")]
    [SerializeField] private GameObject[] mapBackGrounds = new GameObject[3];
    [Header("저장 값")]
    [SerializeField] public StageState nowStageState= StageState.Forest;
    [Header("보스 클리어 체크")]
    public bool bossClear = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //보스를 잡고 스테이지를 넘어가는 모든 장면 연출.
    public void NextStage()
    {
        if (!bossClear)
        {
            return;
        }

        DOVirtual.DelayedCall(3, () =>
        {
            Book.instance.bookAnimator.SetTrigger("turnPageToRight");

            BackgroundSetting(nowStageState);
            MapState.InstanceMap.SpawnMap();
        });
    }

    public void SaveStageCount()
    {

    }

    //맵 배경 변경 함수
    public void BackgroundSetting(StageState stageState)
    {
        for (int i = 0; i < mapBackGrounds.Length; i++)
        {
            mapBackGrounds[i].SetActive(false);
        }

        switch (stageState)
        {
            case StageState.Forest:
                mapBackGrounds[0].SetActive(true);
                break;
            case StageState.Sea:
                mapBackGrounds[1].SetActive(true);
                break;
            case StageState.Cave:
                mapBackGrounds[2].SetActive(true);
                break;
            case StageState.MagicTower:
                mapBackGrounds[1].SetActive(true);
                Debug.LogWarning("일단 마탑 배경 없어서 바다로 설정");
                break;
            default:
                Debug.LogWarning("스테이지 변경 오류");
                break;
        }
    }
}