using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burning : KeywordSup
{
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        target.Burn(2);
    }
}
