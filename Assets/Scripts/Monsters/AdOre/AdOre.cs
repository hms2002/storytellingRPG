using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdOre : Monster
{
    private int _oreStack = 0;

    public int oreStack
    {
        get { return _oreStack; }
        set { _oreStack = value; }
    }


    void Start()
    {
        MAX_HP = 100;
        hp = MAX_HP;
    }
}
