using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSetDatabase : MonoBehaviour
{
    public static MonsterSetDatabase monsterSetDatabase;
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
