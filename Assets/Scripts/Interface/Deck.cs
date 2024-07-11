using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    //키워드를 담는 덱 뭉치
    [Header("키워드 덱 리스트")]
    [SerializeField]
    private List<GameObject> supportDeck;
    [SerializeField]
    private List<GameObject> mainDeck;

    [Header("덱 사이즈")]
    [SerializeField]
    private int supportDeckSize = 0;
    [SerializeField]
    private int mainDeckSize = 0;

    private void Awake()
    {
        supportDeckSize = supportDeck.Count;
        mainDeckSize = mainDeck.Count;
    }


    public GameObject DrawSupportKeyword()
    {
        int deckIndex = Random.Range(0, supportDeckSize - 1);
        Debug.Log("sup 사이즈" + supportDeckSize);

        return supportDeck[deckIndex];
    } // 뽑은 키워드는 덱에서 삭제해야 함 

    public GameObject DrawMainKeyword()
    {
        int deckIndex = Random.Range(0, mainDeckSize - 1);
        Debug.Log("main 사이즈" + mainDeckSize);

        return mainDeck[deckIndex];
    } // 뽑은 키워드는 덱에서 삭제해야 함
}