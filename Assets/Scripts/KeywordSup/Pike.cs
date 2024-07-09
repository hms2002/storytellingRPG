using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pike : KeywordSup
{
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.PikeControl(3);
    }
}
