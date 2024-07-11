using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : KeywordMain
{
    private void Awake()
    {
        SetKeywordColor(BLUE);
        keywordProtect = 10;
        keyWordTension = 6;
    }
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.protect = 10;
        sentence.tension += keyWordTension;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
