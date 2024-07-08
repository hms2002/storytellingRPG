using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KeywordMain
{
    protected int damage = 0;
    protected int additionalDamage = 0;
    protected int repeatCount = 1;

    public abstract void Execute(Actor self, Actor target);
    public void AddDamage(int addRate)
    {
        additionalDamage += addRate;
    }
    public void UpRepeatCount(int repeatRate)
    {
        repeatCount += repeatRate;
    }
}
