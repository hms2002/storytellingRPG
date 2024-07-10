using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharp : KeywordSup
{
    private void Awake()
    {
        SetKeywordColor(RED);
    }
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.damage += (2);
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }
}
