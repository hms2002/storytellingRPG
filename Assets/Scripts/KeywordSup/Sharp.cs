using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharp : KeywordSup
{
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.DamageControl(2);
    }
}
