using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scales : KeywordMain
{
    private void Awake()
    {
        SetKeywordColor(BLUE);
        keywordProtect = 30;
    }
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.protect += keywordProtect;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
