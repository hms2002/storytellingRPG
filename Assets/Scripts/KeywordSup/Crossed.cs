using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossed : KeywordSup
{
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        Debug.Log("������ �ߵ�");

        sentence.DamageControl(caster.GetProtect());
    }
}
