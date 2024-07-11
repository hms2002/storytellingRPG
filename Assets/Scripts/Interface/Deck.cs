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


    public void InitDeck()
    {
        supportDeck = new List<GameObject>();
        mainDeck = new List<GameObject>();
    }


    public GameObject DrawSupportKeyword()
    {
        GameObject supportDeckTemp; // 랜덤으로 뽑은 서포트 키워드를 덱에서 지우기 위해 잠시 담아놓을 공간 
        int deckIndex = Random.Range(0, supportDeck.Count - 1); // 무작위 키워드 추출

        supportDeckTemp = supportDeck[deckIndex];
        supportDeck.RemoveAt(deckIndex);

        return supportDeckTemp;
    }

    public GameObject DrawMainKeyword()
    {
        GameObject mainDeckTemp; // 랜덤으로 뽑은 메인 키워드를 덱에서 지우기 위해 잠시 담아놓을 공간 
        int deckIndex = Random.Range(0, mainDeck.Count - 1); // 무작위 키워드 추출

        mainDeckTemp = mainDeck[deckIndex];
        mainDeck.RemoveAt(deckIndex);

        return mainDeckTemp;
    }

    public bool IsSupDeckEmpty()
    {
        if (supportDeck.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsMainDeckEmpty()
    {
        if (mainDeck.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #region Getter, Setter 함수들
    public void AddSupKeywordOnDeck(GameObject keyword)
    {
        if (keyword.GetComponent<KeywordSup>() == null) return;

        supportDeck.Add(keyword);
    }

    public void AddMainKeywordOnDeck(GameObject keyword)
    {
        if (keyword.GetComponent<KeywordMain>() == null) return;

        mainDeck.Add(keyword);
    }

    public int GetSupportDeckSize()
    {
        return supportDeck.Count;
    }

    public int GetMainDeckSize()
    {
        return mainDeck.Count;
    }

    #endregion
}