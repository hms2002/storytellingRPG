using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeakAttack : KeywordMain
{
    private void Awake()
    {
        SetKeywordColor(BLUE);
        keywordProtect = 30;
        keyWordTension = -24;
    }
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.protect += keywordProtect;
        sentence.tension += keyWordTension;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
