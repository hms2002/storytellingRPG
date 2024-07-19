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
    public List<Actor> GetSet1()
    {
        List<Actor> returnList = new List<Actor>();
        foreach (GameObject monster in monsterSet1)
        {
            returnList.Add(Instantiate(monster).GetComponent<Actor>());
        }
        return returnList;
    }
    public List<Actor> GetSet2()
    {
        List<Actor> returnList = new List<Actor>();
        foreach (GameObject monster in monsterSet2)
        {
            returnList.Add(Instantiate(monster).GetComponent<Actor>());
        }
        return returnList;
    }
    public List<Actor> GetSet3()
    {
        List<Actor> returnList = new List<Actor>();
        foreach (GameObject monster in monsterSet3)
        {
            returnList.Add(Instantiate(monster).GetComponent<Actor>());
        }
        return returnList;
    }
    public List<Actor> GetSet4()
    {
        List<Actor> returnList = new List<Actor>();
        foreach (GameObject monster in monsterSet4)
        {
            returnList.Add(Instantiate(monster).GetComponent<Actor>());
        }
        return returnList;
    }
}
