using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : KeywordSup
{
    private void Awake()
    {
        SetKeywordColor(RED);
    }
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.repeatStack += (1);
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
