using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twingkle : KeywordSup
{
    private void Awake()
    {
        SetKeywordColor(BLUE);
        keywordProtect = 5;
        keyWordTension = -6;
    }

    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.protect = keywordProtect;
        sentence.tension += keyWordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
