using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : KeywordMain
{
    public override void Execute(Actor self, Actor target, Sentence sentence)
    {
        Debug.Log("Fist ¹ßµ¿");
        int fistDamage = 1;
        sentence.DamageControl(fistDamage);
    }
}
