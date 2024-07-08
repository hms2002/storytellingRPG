using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : KeywordMain
{
    private void Awake()
    {
        damage = 2;
    }
    public override void Execute(Actor self, Actor target)
    {
        Debug.Log("Fist �ߵ�");
        for (int i = 0; i < repeatCount; i++)
        {
            Debug.Log("������ ������" + (damage + additionalDamage));
            target.Damaged(damage + additionalDamage, DamageType.Beat);
        }
    }
}
