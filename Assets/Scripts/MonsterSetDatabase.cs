using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSetDatabase : MonoBehaviour
{
    public static MonsterSetDatabase monsterSetDatabase;

    internal List<GameObject> selectedMonsterSet;
    List<GameObject>[] stage1MonsterLists = new List<GameObject>[8];
    public List<GameObject> monsterSet1;
    public List<GameObject> monsterSet2;
    public List<GameObject> monsterSet3;
    public List<GameObject> monsterSet4;
    public List<GameObject> monsterSet5;
    public List<GameObject> monsterSet6;
    public List<GameObject> monsterSet7;
    public List<GameObject> monsterSet8;

    private void Awake()
    {
        if (monsterSetDatabase == null)
        {
            monsterSetDatabase = this;
        }
        stage1MonsterLists[0] = monsterSet1;
        stage1MonsterLists[1] = monsterSet2;
        stage1MonsterLists[2] = monsterSet3;
        stage1MonsterLists[3] = monsterSet4;
        stage1MonsterLists[4] = monsterSet5;
        stage1MonsterLists[5] = monsterSet6;
        stage1MonsterLists[6] = monsterSet7;
        stage1MonsterLists[7] = monsterSet8;
    }
    public List<Monster> GetSelectedSet()
    {
        if (selectedMonsterSet == null) return null;
        
        List<Monster> returnList = new List<Monster>();

        foreach (GameObject monster in selectedMonsterSet)
        {
            returnList.Add(Instantiate(monster).GetComponent<Monster>());
        }
        return returnList;
    }

    static int monsterWaveIdx = 0;
    /// <summary>
    /// 첫번째 스테이지 인덱스는 1, 두번째는 2......
    /// </summary>
    /// <param name="stageNum"></param>
    /// <param name="nodeType"></param>
    internal void SettingSelectedSet(int stageNum, Map.NodeType nodeType)
    {
        switch(stageNum)
        {
            case 1:

                switch(nodeType)
                {
                    case Map.NodeType.NomalMonsterNode:
                        //int randomIdx = Random.Range(0, stage1MonsterLists.Length);
                        //selectedMonsterSet = stage1MonsterLists[randomIdx];
                        selectedMonsterSet = stage1MonsterLists[monsterWaveIdx++];
                        break;
                    case Map.NodeType.BossNode:
                        break;
                }

                break;
            case 2:

                switch (nodeType)
                {
                    case Map.NodeType.NomalMonsterNode:
                        break;
                    case Map.NodeType.BossNode:
                        break;
                }
                break;
            case 3:

                switch (nodeType)
                {
                    case Map.NodeType.NomalMonsterNode:
                        break;
                    case Map.NodeType.BossNode:
                        break;
                }
                break;
        }
    }

    public List<Monster> GetSet1()
    {
        List<Monster> returnList = new List<Monster>();
        foreach (GameObject monster in monsterSet1)
        {
            returnList.Add(Instantiate(monster).GetComponent<Monster>());
        }
        return returnList;
    }
    public List<Monster> GetSet2()
    {
        List<Monster> returnList = new List<Monster>();
        foreach (GameObject monster in monsterSet2)
        {
            returnList.Add(Instantiate(monster).GetComponent<Monster>());
        }
        return returnList;
    }
    public List<Monster> GetSet3()
    {
        List<Monster> returnList = new List<Monster>();
        foreach (GameObject monster in monsterSet3)
        {
            returnList.Add(Instantiate(monster).GetComponent<Monster>());
        }
        return returnList;
    }
    public List<Monster> GetSet4()
    {
        List<Monster> returnList = new List<Monster>();
        foreach (GameObject monster in monsterSet4)
        {
            returnList.Add(Instantiate(monster).GetComponent<Monster>());
        }
        return returnList;
    }
}
