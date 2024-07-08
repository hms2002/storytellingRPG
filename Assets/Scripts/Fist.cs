using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : KeywordMain
{
    public override void Execute(Actor self, Actor target)
    {
        for(int i = 0; i < repeatCount; i++)
        {
            target.Damaged(damage + additionalDamage, DamageType.Beat);
        }
    }
}
