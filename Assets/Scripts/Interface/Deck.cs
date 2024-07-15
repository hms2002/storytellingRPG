using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
#.��ũ��Ʈ ����

- Ű������� ��� �ִ� �⺻���� ������ ����
- ������ Ű������� ����� �������� ������ ����
*/

public class Deck : MonoBehaviour
{
    [Header("Ű���� �� ����Ʈ")]
    [SerializeField] private List<GameObject> supportDeck;  // Support Ű���带 ��� �� ����Ʈ
    [SerializeField] private List<GameObject> mainDeck;     // Main Ű���带 ��� �� ����Ʈ


    /*==================================================================================================================================*/

    // ������ ����Ʈ �ʱ�ȭ
    public void InitDeck()
    {
        supportDeck = new List<GameObject>();
        mainDeck = new List<GameObject>();
    }

    // Support Ű���� 1�� ���� ��ο�
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

    // Main Ű���� 1�� ���� ��ο�
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

    // �⺻ Support ���� ����ִ��� Ȯ��
    public bool IsSupDeckEmpty()
    {
        // Support �� ����Ʈ�� ����ִٸ� return true, �ϳ� �̻� ä���� �ִٸ� return false
        if (supportDeck.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // �⺻ Main ���� ����ִ��� Ȯ��
    public bool IsMainDeckEmpty()
    {
        // Main �� ����Ʈ�� ����ִٸ� return true, �ϳ� �̻� ä���� �ִٸ� return false
        if (mainDeck.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #region Deck Ŭ������ Getter, Setter �Լ���
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