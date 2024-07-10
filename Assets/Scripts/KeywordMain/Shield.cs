using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : KeywordMain
{
    private void Awake()
    {
        SetKeywordColor(BLUE);
        keywordProtect = 5;
    }
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.protect += 5;
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}
