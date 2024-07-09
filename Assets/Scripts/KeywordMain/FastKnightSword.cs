using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastKnightSword : KeywordMain
{
    private void Awake()
    {
        SetKeywordColor(RED);
    }
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.SetXRepeat(2);
        sentence.DamageControl(2);
    }

    public override void Check(KeywordSup keywordSup)
    {

    }
}
