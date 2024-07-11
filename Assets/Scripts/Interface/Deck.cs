using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    //Ű���带 ��� �� ��ġ
    [Header("Ű���� �� ����Ʈ")]
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
        GameObject supportDeckTemp; // �������� ���� ����Ʈ Ű���带 ������ ����� ���� ��� ��Ƴ��� ���� 
        int deckIndex = Random.Range(0, supportDeck.Count - 1); // ������ Ű���� ����

        supportDeckTemp = supportDeck[deckIndex];
        supportDeck.RemoveAt(deckIndex);

        return supportDeckTemp;
    }

    public GameObject DrawMainKeyword()
    {
        GameObject mainDeckTemp; // �������� ���� ���� Ű���带 ������ ����� ���� ��� ��Ƴ��� ���� 
        int deckIndex = Random.Range(0, mainDeck.Count - 1); // ������ Ű���� ����

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

    #region Getter, Setter �Լ���
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