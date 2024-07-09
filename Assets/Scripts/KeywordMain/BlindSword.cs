using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindSword : KeywordMain
{
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.AdditionalStack(1);
    }
}
