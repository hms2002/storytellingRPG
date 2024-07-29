using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 키워드들을 담는 기본덱, 버려진 키워드들을 담는 무덤덱 역할 수행
/// </summary>
public class Deck : MonoBehaviour
{
    [Header("키워드 덱 리스트")]
    [SerializeField] private List<GameObject> supportDeck;  // Support 키워드를 담는 덱 리스트
    [SerializeField] private List<GameObject> mainDeck;     // Main 키워드를 담는 덱 리스트


    /*==================================================================================================================================*/


    /// <summary>
    /// 무덤덱 리스트 초기화 메소드
    /// </summary>
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

    /// <summary>
    /// Support 키워드 1개를 랜덤으로 뽑아 드로우하는 메소드
    /// </summary>
    /// <returns>랜덤으로 드로우된 Support 키워드</returns>
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

    /// <summary>
    /// Main 키워드 1개를 랜덤으로 뽑아 드로우하는 메소드
    /// </summary>
    /// <returns>랜덤으로 드로우된 Main 키워드</returns>
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

    /// <summary>
    /// Support, Main덱 리스트 셔플 메소드
    /// </summary>
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

    /// <summary>
    /// Support 기본덱이 비어있는지 확인하는 메소드
    /// </summary>
    /// <returns>비어있으면 true, 하나라도 채워져 있으면 false</returns>
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

    /// <summary>
    /// Main 기본덱이 비어있는지 확인하는 메소드
    /// </summary>
    /// <returns>비어있으면 true, 하나라도 채워져 있으면 false</returns>
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

    public void DisCardByTextSource(TextMeshProUGUI source)
    {
        foreach(GameObject i in mainDeck)
        {
            if (i.GetComponent<Keyword>().nameText.text == source.text)
            {
                mainDeck.Remove(i);
                break;
            } 
        }
        foreach (GameObject i in supportDeck)
        {
            if (i.GetComponent<Keyword>().nameText.text == source.text)
            {
                supportDeck.Remove(i);
                break;
            }
        }
    }
    #endregion
}