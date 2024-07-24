using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeywordUIMovement : MonoBehaviour
{
    public Transform mainDeckPivot;
    public Transform usedDeckPivot;
    public Transform selectedPivot;
    public Vector2 selectedPivotOriginPos;

    public Vector2 centerPos;
    public int padding = 2;

    List<GameObject> keywords;
    GameObject selectedKeyword;

    Vector2[] objPos = new Vector2[10];
    bool stretchKeywordsFlag = false;
    bool moveSelectedKeywordFlag = false;
    bool moveUsedKeywordFlag = false;

    public static KeywordUIMovement instance;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }

    public void StretchKeywords(List<GameObject> _keywords)
    {
        keywords = new List<GameObject>(_keywords);
        int length = keywords.Count;

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

    public void ThrowKwyword()
    {

    }
    float curTime = 0;
    public const float MOVE_TIME = 0.5f;

    Vector2 startSize = new Vector2(0, 0);
    Vector2 endSize = new Vector2(0.01f, 0.01f);
    private void Update()
    {
        if(stretchKeywordsFlag)
        {
            curTime += Time.deltaTime;
            for(int i = 0; i < keywords.Count; i++)
            {
                keywords[i].transform.position = Vector2.Lerp(mainDeckPivot.position, objPos[i], curTime / MOVE_TIME);
                keywords[i].transform.localScale = Vector2.Lerp(startSize, endSize, curTime / MOVE_TIME);
            }
            if (curTime > MOVE_TIME)
            {
                stretchKeywordsFlag = false;
                curTime = 0;
            }
        }
        if(moveSelectedKeywordFlag)
        {
            curTime += Time.deltaTime;
            selectedKeyword.transform.position = Vector2.Lerp(selectedPivotOriginPos, selectedPivot.position, curTime / MOVE_TIME);
            selectedKeyword.transform.localScale = Vector2.Lerp(endSize, startSize, curTime / MOVE_TIME);
            if (curTime > MOVE_TIME)
            {
                moveSelectedKeywordFlag = false;
                moveUsedKeywordFlag = true;
                curTime = 0;
                Destroy(selectedKeyword);
            }
        }
        if (moveUsedKeywordFlag)
        {
            curTime += Time.deltaTime;
            for (int i = 0; i < keywords.Count; i++)
            {
                keywords[i].transform.position = Vector2.Lerp(objPos[i], usedDeckPivot.position, curTime / MOVE_TIME);
                keywords[i].transform.localScale = Vector2.Lerp(endSize, startSize, curTime / MOVE_TIME);
            }
            if (curTime > MOVE_TIME)
            {
                moveUsedKeywordFlag = false;
                curTime = 0;
                for (int i = 0; i < keywords.Count; i++)
                {
                    Destroy(keywords[i]);
                }
            }
        }
    }

    public  void MoveSelectedKeyword(Keyword keywordSup)
    {
        for(int i = 0; i < keywords.Count; i++)
        {
            objPos[i] = keywords[i].transform.position;
            if(keywordSup.gameObject == keywords[i])
            {
                keywords.RemoveAt(i);
            }
        }
        selectedKeyword = keywordSup.gameObject;
        selectedPivotOriginPos = selectedKeyword.transform.position;
        moveSelectedKeywordFlag = true;
    }
}
