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
    private int supportDeckSize = 7;
    [SerializeField]
    private int mainDeckSize = 3;

    private void Awake()
    {
        supportDeck = new List<GameObject>(supportDeckSize);
        mainDeck = new List<GameObject>(mainDeckSize);
        Debug.Log("카드 드로우" + supportDeck.Count);
    }

    public GameObject DrawSupportKeyword()
    {
        int deckIndex = Random.Range(0, supportDeckSize - 1);

        return supportDeck[deckIndex];
    } // 뽑은 키워드는 덱에서 삭제해야 함 

    public GameObject DrawMainKeyword()
    {
        int deckIndex = Random.Range(0, mainDeckSize - 1);

        return mainDeck[deckIndex];
    } // 뽑은 키워드는 덱에서 삭제해야 함
}