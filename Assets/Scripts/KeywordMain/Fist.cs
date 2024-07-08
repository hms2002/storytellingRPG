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
        Debug.Log("Fist 발동");
        for (int i = 0; i < repeatCount; i++)
        {
            Debug.Log("입히는 데미지" + (damage + additionalDamage));
            target.Damaged(damage + additionalDamage, DamageType.Beat);
        }
    }
}
