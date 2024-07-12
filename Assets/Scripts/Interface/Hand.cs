using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
#.��ũ��Ʈ ����



#.������ �ִ� ���


*/
public enum CanvasType
{
    SupportCanvas,
    MainCanvas
}

public class Hand : MonoBehaviour
{
    private GameObject keywordPrefab;

    private List<GameObject> supportHand = new List<GameObject>();      // Support Ű���� ��ü Ÿ��
    private List<GameObject> mainHand = new List<GameObject>();         // Main Ű���� ��ü Ÿ��

    private GameObject[] keywordCanvas;                                 // Support, Main Ű���带 ��� Ű���� ĵ����

    [Header("�ڵ� ũ��")]
    [SerializeField] private const int HANDSIZE = 3;                    // �÷��� ��ü�� ������ �ϴ� Ű���� ����

    private Vector2 createLocation = new Vector2(0, -930);              // Ű���� ���� ��ġ


    /*==================================================================================================================================*/

    private void Awake()
    {
        keywordCanvas = GameObject.FindGameObjectsWithTag("KeyWordCanvas");
    }


    // keywordData �� Support Ű������� ��üȭ ���� supportHand ����Ʈ�� ���� �ִ� ���
    private void FillSupHand()
    {
        for (int i = 0; i < HANDSIZE; i++)
        {
            //keywordPrefab = 

            /*supportHand.Add(keywordPrefab);
            Instantiate(supportHand[i], createLocation,Quaternion.identity, keywordCanvas[0].transform);*/
        }
    }

    // keywordData �� Main Ű������� ��üȭ ���� MainHand ����Ʈ�� ���� �ִ� ���
    private void FillMainHand()
    {

    }
}
