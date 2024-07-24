using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
#.스크립트 설명

- 키워드들을 담고 있는 기본덱의 역할을 수행
- 버려진 키워드들이 담겨질 무덤덱의 역할을 수행
*/

public class Deck : MonoBehaviour
{
    [Header("키워드 덱 리스트")]
    [SerializeField] private List<GameObject> supportDeck;  // Support 키워드를 담는 덱 리스트
    [SerializeField] private List<GameObject> mainDeck;     // Main 키워드를 담는 덱 리스트


    /*==================================================================================================================================*/


    // 무덤덱 리스트 초기화
    public void InitDeck()
    {
        supportDeck = new List<GameObject>();
        mainDeck = new List<GameObject>();
    }
    public void InitDeck(Deck _deck)
    {
        supportDeck = new List<GameObject>(_deck.supportDeck);
        mainDeck = new List<GameObject>(_deck.mainDeck);
    }

    // Support 키워드 1개 랜덤 드로우
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

    // Main 키워드 1개 랜덤 드로우
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

    public void ShuffleDeck()
    {
        GameObject temp;

        int random1, random2;

        // Support덱 리스트 셔플
        for (int i = 0; i < supportDeck.Count; ++i)
        {
            random1 = Random.Range(0, supportDeck.Count);
            random2 = Random.Range(0, supportDeck.Count);

            temp = supportDeck[random1];
            supportDeck[random1] = supportDeck[random2];
            supportDeck[random2] = temp;
        }

        // Main덱 리스트 셔플
        for (int i = 0; i < mainDeck.Count; ++i)
        {
            random1 = Random.Range(0, mainDeck.Count);
            random2 = Random.Range(0, mainDeck.Count);

            temp = mainDeck[random1];
            mainDeck[random1] = mainDeck[random2];
            mainDeck[random2] = temp;
        }
    }

    // 기본 Support 덱이 비어있는지 확인
    public bool IsSupDeckEmpty()
    {
        // Support 덱 리스트가 비어있다면 return true, 하나 이상 채워져 있다면 return false
        if (supportDeck.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // 기본 Main 덱이 비어있는지 확인
    public bool IsMainDeckEmpty()
    {
        // Main 덱 리스트가 비어있다면 return true, 하나 이상 채워져 있다면 return false
        if (mainDeck.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #region Deck 클래스의 Getter, Setter 함수들
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

    public int GetSupDeckSize() { return supportDeck.Count; }
    public int GetMainDeckSize() { return mainDeck.Count; }
    #endregion
}