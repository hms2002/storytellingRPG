using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeywordUIMovement : MonoBehaviour
{
    public Transform mainDeckPivot;
    public Transform UsedDeckPivot;
    public Vector2 centerPos;
    public int padding = 2;
    public GameObject[] keywords;
    
    Vector2[] objPos = new Vector2[10];
    bool stretchKeywordsFlag = false;

    private void Start()
    {
        StretchKeywords();
    }

    public void StretchKeywords()
    {
        int length = keywords.Length;

        int cnt = 0;
        if (length % 2 == 1)
        {
            for (int i = length / 2; i >= 0; i--)
            {
                objPos[i] = centerPos - new Vector2(2 * cnt++, 0);
            }
            cnt = 1;
            for (int i = length / 2 + 1; i < length; i++)
            {
                objPos[i] = centerPos + new Vector2(2 * cnt++, 0);
            }
        }
        if (length % 2 == 0)
        {
            cnt = 0;
            for (int i = length / 2; i < length; i++)
            {
                objPos[i] = centerPos + new Vector2((padding / 2), 0) + new Vector2(padding * cnt++, 0);
            }
            cnt = 0;
            for (int i = length / 2 - 1; i >= 0; i--)
            {
                objPos[i] = centerPos - new Vector2((padding / 2), 0) - new Vector2(padding * cnt++, 0);
            }
        }
        stretchKeywordsFlag = true;
    }


    float curTime = 0;
    public const float MOVE_TIME = 0.5f;
    private void Update()
    {
        if(stretchKeywordsFlag == true)
        {
            curTime += Time.deltaTime;
            for(int i = 0; i < keywords.Length; i++)
            {
                keywords[i].transform.position = Vector2.Lerp(mainDeckPivot.position, objPos[i], curTime / MOVE_TIME);
            }
            if (curTime > MOVE_TIME)
            {
                stretchKeywordsFlag = false;
                curTime = 0;
            }
        }
    }
}
