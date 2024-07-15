using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
#.스크립트 설명

- 

*/

public class Hand : MonoBehaviour
{
    private List<GameObject> supKeywordPrefab = new List<GameObject>();
    private List<GameObject> mainKeywordPrefab = new List<GameObject>();

    private List<GameObject> supportHand = new List<GameObject>();      // Support 키워드 실체 타입
    private List<GameObject> mainHand = new List<GameObject>();         // Main 키워드 실체 타입

    [Header("핸드의 양")]
    [SerializeField] private const int _HANDSIZE = 3;                   // 플레이 주체가 가져야 하는 키워드 개수
    public int HANDSIZE { get { return _HANDSIZE; } }

    private Vector2 createLocation = new Vector2(0, -930);              // 키워드 생성 위치


    /*==================================================================================================================================*/


    // keywordData 속 Support 키워드들을 실체화 시켜 supportHand 리스트에 집어 넣는 기능
    public void SubstantiateSupKeywordData()
    {
        for (int i = 0; i < HANDSIZE; i++)
        {
            supportHand.Add(Instantiate(supKeywordPrefab[i], createLocation, Quaternion.identity, CanvasData.canvasData.handCanvas.transform));
        }
    }

    // keywordData 속 Main 키워드들을 실체화 시켜 MainHand 리스트에 집어 넣는 기능
    public void SubstantiateMainKeywordData()
    {
        for (int i = 0; i < HANDSIZE; i++)
        {
            mainHand.Add(Instantiate(mainKeywordPrefab[i], createLocation, Quaternion.identity, CanvasData.canvasData.handCanvas.transform));
        }
    }

    // Hand에 있는 Support 키워드들을 무덤덱으로 버리는 기능
    public GameObject ThrowSupKeyword(int index)
    {
        GameObject supKeywordPrefab_temp = supKeywordPrefab[index];

        supKeywordPrefab.RemoveAt(index);
        
        Destroy(supportHand[index]);
        supportHand.RemoveAt(index);

        return supKeywordPrefab_temp;
    }

    // Hand에 있는 Main 키워드들을 무덤덱으로 버리는 기능
    public GameObject ThrowMainKeyword(int index)
    {
        GameObject mainKeywordPrefab_temp = mainKeywordPrefab[index];

        mainKeywordPrefab.RemoveAt(index);

        Destroy(mainHand[index]);
        mainHand.RemoveAt(index);

        return mainKeywordPrefab_temp;
    }


    public void SetSupPrefabInfo(GameObject gameObject) { supKeywordPrefab.Add(gameObject); }
    public void SetMainPrefabInfo(GameObject gameObject) { mainKeywordPrefab.Add(gameObject); }
}
