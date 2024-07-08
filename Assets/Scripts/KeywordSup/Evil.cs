using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evil : KeywordSup
{
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.WeakenControl(1);
    }
}
