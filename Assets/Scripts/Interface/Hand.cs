using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
#.스크립트 설명



#.가지고 있는 기능


*/
public enum CanvasType
{
    SupportCanvas,
    MainCanvas
}

public class Hand : MonoBehaviour
{
    private GameObject keywordPrefab;

    private List<GameObject> supportHand = new List<GameObject>();      // Support 키워드 실체 타입
    private List<GameObject> mainHand = new List<GameObject>();         // Main 키워드 실체 타입

    private GameObject[] keywordCanvas;                                 // Support, Main 키워드를 담는 키워드 캔버스

    [Header("핸드 크기")]
    [SerializeField] private const int HANDSIZE = 3;                    // 플레이 주체가 가져야 하는 키워드 개수

    private Vector2 createLocation = new Vector2(0, -930);              // 키워드 생성 위치


    /*==================================================================================================================================*/

    private void Awake()
    {
        keywordCanvas = GameObject.FindGameObjectsWithTag("KeyWordCanvas");
    }


    // keywordData 속 Support 키워드들을 실체화 시켜 supportHand 리스트에 집어 넣는 기능
    private void FillSupHand()
    {
        for (int i = 0; i < HANDSIZE; i++)
        {
            //keywordPrefab = 

            /*supportHand.Add(keywordPrefab);
            Instantiate(supportHand[i], createLocation,Quaternion.identity, keywordCanvas[0].transform);*/
        }
    }

    // keywordData 속 Main 키워드들을 실체화 시켜 MainHand 리스트에 집어 넣는 기능
    private void FillMainHand()
    {

    }
}
