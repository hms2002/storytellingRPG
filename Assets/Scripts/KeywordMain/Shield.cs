using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : KeywordMain
{
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.ProtectControl(5);
    }
}
