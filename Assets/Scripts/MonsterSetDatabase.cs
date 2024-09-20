using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSetDatabase : MonoBehaviour
{
    public static MonsterSetDatabase monsterSetDatabase;

    internal List<GameObject> selectedMonsterSet;
    List<GameObject>[] stage1_NomalMonsterLists = new List<GameObject>[7];
    List<GameObject>[] stage1_EliteMonsterLists = new List<GameObject>[3];
    List<GameObject>[] stage1_BossMonsterLists = new List<GameObject>[1];
    List<int> stage1_NomalMonsterRandomTable;
    List<int> stage1_EliteMonsterRandomTable;

    public List<GameObject> s1_NomalMonsterSet1;
    public List<GameObject> s1_NomalMonsterSet2;
    public List<GameObject> s1_NomalMonsterSet3;
    public List<GameObject> s1_NomalMonsterSet4;
    public List<GameObject> s1_NomalMonsterSet5;
    public List<GameObject> s1_NomalMonsterSet6;
    public List<GameObject> s1_NomalMonsterSet7;
    /*    public List<GameObject> s1_NomalMonsterSet8;
        public List<GameObject> s1_NomalMonsterSet9;*/

    public List<GameObject> s1_EliteMonsterSet1;
    public List<GameObject> s1_EliteMonsterSet2;
    public List<GameObject> s1_EliteMonsterSet3;


    public List<GameObject> s1_BossMonsterSet1;

    List<GameObject>[] stage2_NomalMonsterLists = new List<GameObject>[6];
    List<GameObject>[] stage2_EliteMonsterLists = new List<GameObject>[3];
    List<GameObject>[] stage2_BossMonsterLists = new List<GameObject>[1];
    List<int> stage2_NomalMonsterRandomTable;
    List<int> stage2_EliteMonsterRandomTable;

    public List<GameObject> s2_NomalMonsterSet1;
    public List<GameObject> s2_NomalMonsterSet2;
    public List<GameObject> s2_NomalMonsterSet3;
    public List<GameObject> s2_NomalMonsterSet4;
    public List<GameObject> s2_NomalMonsterSet5;
    public List<GameObject> s2_NomalMonsterSet6;
/*    public List<GameObject> s2_NomalMonsterSet7;*/

    public List<GameObject> s2_EliteMonsterSet1;
    public List<GameObject> s2_EliteMonsterSet2;
    public List<GameObject> s2_EliteMonsterSet3;

    public List<GameObject> s2_BossMonsterSet1;

    List<GameObject>[] stage3_NomalMonsterLists = new List<GameObject>[7];
    List<GameObject>[] stage3_EliteMonsterLists = new List<GameObject>[2];
    List<GameObject>[] stage3_BossMonsterLists = new List<GameObject>[1];
    List<int> stage3_NomalMonsterRandomTable;
    List<int> stage3_EliteMonsterRandomTable;

    public List<GameObject> s3_NomalMonsterSet1;
    public List<GameObject> s3_NomalMonsterSet2;
    public List<GameObject> s3_NomalMonsterSet3;
    public List<GameObject> s3_NomalMonsterSet4;
    public List<GameObject> s3_NomalMonsterSet5;
    public List<GameObject> s3_NomalMonsterSet6;
    public List<GameObject> s3_NomalMonsterSet7;

    public List<GameObject> s3_EliteMonsterSet1;
    public List<GameObject> s3_EliteMonsterSet2;

    public List<GameObject> s3_BossMonsterSet1;

    List<GameObject>[] stage4_NomalMonsterLists = new List<GameObject>[8];
    List<GameObject>[] stage4_EliteMonsterLists = new List<GameObject>[2];
    List<GameObject>[] stage4_BossMonsterLists = new List<GameObject>[1];
    List<int> stage4_NomalMonsterRandomTable;
    List<int> stage4_EliteMonsterRandomTable;

    public List<GameObject> s4_NomalMonsterSet1;
    public List<GameObject> s4_NomalMonsterSet2;
    public List<GameObject> s4_NomalMonsterSet3;
    public List<GameObject> s4_NomalMonsterSet4;
    public List<GameObject> s4_NomalMonsterSet5;
    public List<GameObject> s4_NomalMonsterSet6;
    public List<GameObject> s4_NomalMonsterSet7;
    public List<GameObject> s4_NomalMonsterSet8;

    public List<GameObject> s4_EliteMonsterSet1;
    public List<GameObject> s4_EliteMonsterSet2;

    public List<GameObject> s4_BossMonsterSet1;


    private void Awake()
    {
        if (monsterSetDatabase == null)
        {
            monsterSetDatabase = this;
        }

        MonsterSetting();
    }

    public void MonsterSetting()
    {
        stage1_NomalMonsterLists[0] = s1_NomalMonsterSet1;
        stage1_NomalMonsterLists[1] = s1_NomalMonsterSet2;
        stage1_NomalMonsterLists[2] = s1_NomalMonsterSet3;
        stage1_NomalMonsterLists[3] = s1_NomalMonsterSet4;
        stage1_NomalMonsterLists[4] = s1_NomalMonsterSet5;
        stage1_NomalMonsterLists[5] = s1_NomalMonsterSet6;
        stage1_NomalMonsterLists[6] = s1_NomalMonsterSet7;
        stage1_NomalMonsterRandomTable = GetRandomSortList(stage1_NomalMonsterLists.Length);

        // 인덱스 outOfRange 때문에 잠궈둠.
        /*        stage1_NomalMonsterLists[7] = s1_NomalMonsterSet8;
                stage1_NomalMonsterLists[8] = s1_NomalMonsterSet9;*/

        stage1_EliteMonsterLists[0] = s1_EliteMonsterSet1;
        stage1_EliteMonsterLists[1] = s1_EliteMonsterSet2;
        stage1_EliteMonsterLists[2] = s1_EliteMonsterSet3;
        stage1_EliteMonsterRandomTable = GetRandomSortList(stage1_EliteMonsterLists.Length);


        stage1_BossMonsterLists[0] = s1_BossMonsterSet1;

        stage2_NomalMonsterLists[0] = s2_NomalMonsterSet1;
        stage2_NomalMonsterLists[1] = s2_NomalMonsterSet2;
        stage2_NomalMonsterLists[2] = s2_NomalMonsterSet3;
        stage2_NomalMonsterLists[3] = s2_NomalMonsterSet4;
        stage2_NomalMonsterLists[4] = s2_NomalMonsterSet5;
        stage2_NomalMonsterLists[5] = s2_NomalMonsterSet6;
        /*        stage2_NomalMonsterLists[6] = s2_NomalMonsterSet7;*/
        stage2_NomalMonsterRandomTable = GetRandomSortList(stage2_NomalMonsterLists.Length);
        
        stage2_EliteMonsterLists[0] = s2_EliteMonsterSet1;
        stage2_EliteMonsterLists[1] = s2_EliteMonsterSet2;
        stage2_EliteMonsterLists[2] = s2_EliteMonsterSet3;
        stage2_EliteMonsterRandomTable = GetRandomSortList(stage2_EliteMonsterLists.Length);

        stage2_BossMonsterLists[0] = s2_BossMonsterSet1;

        stage3_NomalMonsterLists[0] = s3_NomalMonsterSet1;
        stage3_NomalMonsterLists[1] = s3_NomalMonsterSet2;
        stage3_NomalMonsterLists[2] = s3_NomalMonsterSet3;
        stage3_NomalMonsterLists[3] = s3_NomalMonsterSet4;
        stage3_NomalMonsterLists[4] = s3_NomalMonsterSet5;
        stage3_NomalMonsterLists[5] = s3_NomalMonsterSet6;
        stage3_NomalMonsterLists[6] = s3_NomalMonsterSet7;
        stage3_NomalMonsterRandomTable = GetRandomSortList(stage3_NomalMonsterLists.Length);
        
        stage3_EliteMonsterLists[0] = s3_EliteMonsterSet1;
        stage3_EliteMonsterLists[1] = s3_EliteMonsterSet2;
        stage3_EliteMonsterRandomTable = GetRandomSortList(stage3_EliteMonsterLists.Length);

        stage3_BossMonsterLists[0] = s3_BossMonsterSet1;

        stage4_NomalMonsterLists[0] = s4_NomalMonsterSet1;
        stage4_NomalMonsterLists[1] = s4_NomalMonsterSet2;
        stage4_NomalMonsterLists[2] = s4_NomalMonsterSet3;
        stage4_NomalMonsterLists[3] = s4_NomalMonsterSet4;
        stage4_NomalMonsterLists[4] = s4_NomalMonsterSet5;
        stage4_NomalMonsterLists[5] = s4_NomalMonsterSet6;
        stage4_NomalMonsterLists[6] = s4_NomalMonsterSet7;
        stage4_NomalMonsterLists[7] = s4_NomalMonsterSet8;
        stage4_NomalMonsterRandomTable = GetRandomSortList(stage4_NomalMonsterLists.Length);
        
        stage4_EliteMonsterLists[0] = s4_EliteMonsterSet1;
        stage4_EliteMonsterLists[1] = s4_EliteMonsterSet2;
        stage4_EliteMonsterRandomTable = GetRandomSortList(stage4_EliteMonsterLists.Length);

        stage4_BossMonsterLists[0] = s4_BossMonsterSet1;

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

    public void SetSelectedSet(List<GameObject> nomalMonsterList)
    {
        selectedMonsterSet = nomalMonsterList;
    }

    static int nomalTableIdx = 0;
    static int eliteTableIdx = 0;
    /// <summary>
    /// 첫번째 스테이지 인덱스는 1, 두번째는 2......   
    /// </summary>
    /// <param name="stageNum"></param>
    /// <param name="nodeType"></param>
    internal void SettingSelectedSet(StageState stageState, Map.NodeType nodeType)
    {
        int idx;
        switch (stageState)
        {
            case StageState.Forest:

                switch(nodeType)
                {
                    case Map.NodeType.NomalMonsterNode:
                        idx = nomalTableIdx++ % stage1_NomalMonsterLists.Length;
                        selectedMonsterSet = stage1_NomalMonsterLists[stage1_NomalMonsterRandomTable[idx]];
                        break;

                    case Map.NodeType.EliteMonsterNode:
                        idx = eliteTableIdx++ % stage1_EliteMonsterLists.Length;
                        selectedMonsterSet = stage1_EliteMonsterLists[stage1_EliteMonsterRandomTable[idx]];
                        break;

                    case Map.NodeType.BossNode:
                        selectedMonsterSet = stage1_BossMonsterLists[0];
                        foreach (GameObject m in selectedMonsterSet)
                            Debug.Log(m.name);
                        break;
                }

                break;

            case StageState.Cave:
                switch (nodeType)
                {
                    case Map.NodeType.NomalMonsterNode:
                        idx = nomalTableIdx++ % stage2_NomalMonsterLists.Length;
                        selectedMonsterSet = stage2_NomalMonsterLists[stage2_NomalMonsterRandomTable[idx]];
                        break;

                    case Map.NodeType.EliteMonsterNode:
                        idx = eliteTableIdx++ % stage2_EliteMonsterLists.Length;
                        selectedMonsterSet = stage2_EliteMonsterLists[stage2_EliteMonsterRandomTable[idx]];
                        break;

                    case Map.NodeType.BossNode:
                        selectedMonsterSet = stage2_BossMonsterLists[0];
                        break;
                }
                break;

            case StageState.Sea:
                switch (nodeType)
                {
                    case Map.NodeType.NomalMonsterNode:
                        idx = nomalTableIdx++ % stage3_NomalMonsterLists.Length;
                        selectedMonsterSet = stage3_NomalMonsterLists[stage3_NomalMonsterRandomTable[idx]];
                        break;

                    case Map.NodeType.EliteMonsterNode:
                        idx = eliteTableIdx++ % stage3_EliteMonsterLists.Length;
                        selectedMonsterSet = stage3_EliteMonsterLists[stage3_EliteMonsterRandomTable[idx]];
                        break;

                    case Map.NodeType.BossNode:
                        selectedMonsterSet = stage3_BossMonsterLists[0];
                        break;
                }
                break;

            case StageState.MagicTower:
                switch (nodeType)
                {
                    case Map.NodeType.NomalMonsterNode:
                        idx = nomalTableIdx++ % stage4_NomalMonsterLists.Length;
                        selectedMonsterSet = stage4_NomalMonsterLists[stage4_NomalMonsterRandomTable[idx]];
                        break;

                    case Map.NodeType.EliteMonsterNode:
                        idx = eliteTableIdx++ % stage4_EliteMonsterLists.Length;
                        selectedMonsterSet = stage4_EliteMonsterLists[stage4_EliteMonsterRandomTable[idx]];
                        break;

                    case Map.NodeType.BossNode:
                        selectedMonsterSet = stage4_BossMonsterLists[0];
                        break;
                }
                break;
        }
    }

    /*
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
    */

    List<int> GetRandomSortList(int length)
    {
        List<int> temp = new List<int>();

        for(int i = 0; i < length; i++)
        {
            temp.Add(i);
        }
        for(int i = 0; i < length; i++)
        {
            int rand = Random.Range(0, length);
            temp.Add(temp[rand]);
            temp.RemoveAt(rand);
        }

        return temp;
    }
}
