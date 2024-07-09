using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossed : KeywordSup
{
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        Debug.Log("교차된 발동");

        sentence.DamageControl(caster.GetProtect());
    }
}
