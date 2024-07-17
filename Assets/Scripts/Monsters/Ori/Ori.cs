using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ori : Actor
{
    private int _oreStack = 0;

    public int oreStack
    {
        get { return _oreStack; }
        set { _oreStack = value; }
    }

}
