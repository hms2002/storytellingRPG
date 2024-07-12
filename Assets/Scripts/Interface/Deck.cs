using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Deck 클래스는 일반 덱 뭉치가 될 수도 있고, 무덤 덱 뭉치가 될 수도 있다
public class Deck : MonoBehaviour
{
    [Header("키워드 덱 리스트")]
    [SerializeField] private List<GameObject> supportDeck;  // Support 키워드를 담는 덱 리스트
    [SerializeField] private List<GameObject> mainDeck;     // Main 키워드를 담는 덱 리스트


    /*==================================================================================================================================*/


    public void InitDeck()
    {
        supportDeck = new List<GameObject>();
        mainDeck = new List<GameObject>();
    }

    public GameObject DrawSupKeyword()
    {
        // 랜덤으로 뽑은 서포트 키워드를 덱에서 지우기 위해 잠시 담아놓을 공간 
        GameObject supportDeckTemp;

        // 무작위 키워드 추출을 위한 번호 추첨
        int deckIndex = Random.Range(0, supportDeck.Count - 1);

        supportDeckTemp = supportDeck[deckIndex];
        supportDeck.RemoveAt(deckIndex);

        return supportDeckTemp;
    }

    public GameObject DrawMainKeyword()
    {
        // 랜덤으로 뽑은 메인 키워드를 덱에서 지우기 위해 잠시 담아놓을 공간
        GameObject mainDeckTemp;

        // 무작위 키워드 추출을 위한 번호 추첨
        int deckIndex = Random.Range(0, mainDeck.Count - 1);

        mainDeckTemp = mainDeck[deckIndex];
        mainDeck.RemoveAt(deckIndex);

        return mainDeckTemp;
    }

    public bool IsSupDeckEmpty()
    {
        // 서포트 덱 리스트가 비어있다면 return true, 하나 이상 채워져 있다면 return false
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
        // 서포트 덱 리스트가 비어있다면 return true, 하나 이상 채워져 있다면 return false
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

    public int GetSupDeckSize()
    {
        return supportDeck.Count;
    }

    public int GetMainDeckSize()
    {
        return mainDeck.Count;
    }

    #endregion
}