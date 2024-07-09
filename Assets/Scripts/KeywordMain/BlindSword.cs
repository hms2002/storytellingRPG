using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindSword : KeywordMain
{
    private void Awake()
    {
        SetKeywordColor(RED);
    }
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.AdditionalStack(1);
    }

    public override void Check(KeywordSup keywordSup)
    {

    }
}
