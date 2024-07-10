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

    [Header("�� ������")]
    [SerializeField]
    private int supportDeckSize = 7;
    [SerializeField]
    private int mainDeckSize = 3;

    private void Awake()
    {
        supportDeck = new List<GameObject>(supportDeckSize);
        mainDeck = new List<GameObject>(mainDeckSize);
        Debug.Log("ī�� ��ο�" + supportDeck.Count);
    }

    public GameObject DrawSupportKeyword()
    {
        int deckIndex = Random.Range(0, supportDeckSize - 1);

        return supportDeck[deckIndex];
    } // ���� Ű����� ������ �����ؾ� �� 

    public GameObject DrawMainKeyword()
    {
        int deckIndex = Random.Range(0, mainDeckSize - 1);

        return mainDeck[deckIndex];
    } // ���� Ű����� ������ �����ؾ� ��
}