using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
#.��ũ��Ʈ ����



#.������ �ִ� ���


*/

public class Hand : MonoBehaviour
{
    private List<GameObject> supKeywordPrefab = new List<GameObject>();
    private List<GameObject> mainKeywordPrefab = new List<GameObject>();

    private List<GameObject> supportHand = new List<GameObject>();      // Support Ű���� ��ü Ÿ��
    private List<GameObject> mainHand = new List<GameObject>();         // Main Ű���� ��ü Ÿ��

    [Header("�ڵ� ũ��")]
    [SerializeField] private const int _HANDSIZE = 3;                    // �÷��� ��ü�� ������ �ϴ� Ű���� ����
    public int HANDSIZE
    {
        get { return _HANDSIZE; }
    }


    private Vector2 createLocation = new Vector2(0, -930);              // Ű���� ���� ��ġ


    /*==================================================================================================================================*/


    // keywordData �� Support Ű������� ��üȭ ���� supportHand ����Ʈ�� ���� �ִ� ���
    public void FillSupHand()
    {
        for (int i = 0; i < HANDSIZE; i++)
        {
            supportHand.Add(Instantiate(supKeywordPrefab[i], createLocation, Quaternion.identity, CanvasData.canvasData.handCanvas.transform));
        }
    }

    // keywordData �� Main Ű������� ��üȭ ���� MainHand ����Ʈ�� ���� �ִ� ���
    public void FillMainHand()
    {
        for (int i = 0; i < HANDSIZE; i++)
        {
            mainHand.Add(Instantiate(mainKeywordPrefab[i], createLocation, Quaternion.identity, CanvasData.canvasData.handCanvas.transform));
        }
    }

    // 
    public GameObject ThrowSupKeyword(int index)
    {
        GameObject supKeywordPrefab_temp = supKeywordPrefab[index];

        supKeywordPrefab.RemoveAt(index);
        
        Destroy(supportHand[index]);
        supportHand.RemoveAt(index);

        return supKeywordPrefab_temp;
    }

    // 
    public GameObject ThrowMainKeyword(int index)
    {
        GameObject mainKeywordPrefab_temp = mainKeywordPrefab[index];

        mainKeywordPrefab.RemoveAt(index);

        Destroy(mainHand[index]);
        mainHand.RemoveAt(index);

        return mainKeywordPrefab_temp;
    }

    public void SetSupPrefabInfo(GameObject gameObject)
    {
        supKeywordPrefab.Add(gameObject);
    }

    public void SetMainPrefabInfo(GameObject gameObject)
    {
        mainKeywordPrefab.Add(gameObject);
    }
}
