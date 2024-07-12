using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Deck Ŭ������ �Ϲ� �� ��ġ�� �� ���� �ְ�, ���� �� ��ġ�� �� ���� �ִ�
public class Deck : MonoBehaviour
{
    [Header("Ű���� �� ����Ʈ")]
    [SerializeField] private List<GameObject> supportDeck;  // Support Ű���带 ��� �� ����Ʈ
    [SerializeField] private List<GameObject> mainDeck;     // Main Ű���带 ��� �� ����Ʈ


    /*==================================================================================================================================*/


    public void InitDeck()
    {
        supportDeck = new List<GameObject>();
        mainDeck = new List<GameObject>();
    }

    public GameObject DrawSupKeyword()
    {
        // �������� ���� ����Ʈ Ű���带 ������ ����� ���� ��� ��Ƴ��� ���� 
        GameObject supportDeckTemp;

        // ������ Ű���� ������ ���� ��ȣ ��÷
        int deckIndex = Random.Range(0, supportDeck.Count - 1);

        supportDeckTemp = supportDeck[deckIndex];
        supportDeck.RemoveAt(deckIndex);

        return supportDeckTemp;
    }

    public GameObject DrawMainKeyword()
    {
        // �������� ���� ���� Ű���带 ������ ����� ���� ��� ��Ƴ��� ����
        GameObject mainDeckTemp;

        // ������ Ű���� ������ ���� ��ȣ ��÷
        int deckIndex = Random.Range(0, mainDeck.Count - 1);

        mainDeckTemp = mainDeck[deckIndex];
        mainDeck.RemoveAt(deckIndex);

        return mainDeckTemp;
    }

    public bool IsSupDeckEmpty()
    {
        // ����Ʈ �� ����Ʈ�� ����ִٸ� return true, �ϳ� �̻� ä���� �ִٸ� return false
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
        // ����Ʈ �� ����Ʈ�� ����ִٸ� return true, �ϳ� �̻� ä���� �ִٸ� return false
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