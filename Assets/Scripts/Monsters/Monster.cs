using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Actor
{
    protected int _tension = 0;
    public int tension
    {
        get { return _tension; }
        set { _tension = value; }
    }
    internal override void Action(Actor target)
    {
        base.Action(target);

        
    }
}
