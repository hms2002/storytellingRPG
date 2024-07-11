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
        Debug.Log("sup ������" + supportDeckSize);

        return supportDeck[deckIndex];
    } // ���� Ű����� ������ �����ؾ� �� 

    public GameObject DrawMainKeyword()
    {
        int deckIndex = Random.Range(0, mainDeckSize - 1);
        Debug.Log("main ������" + mainDeckSize);

        return mainDeck[deckIndex];
    } // ���� Ű����� ������ �����ؾ� ��
}