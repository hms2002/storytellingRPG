using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continuity : KeywordSup
{
    TumbleBird tumbleBird;
    private void Awake()
    {
        keywordName = "연속";

        SetKeywordColor(BLUE);
        keyWordTension = 10;
    }

    public override void Execute(Actor caster, Actor target)
    {
        tumbleBird = caster as TumbleBird;
        tumbleBird.isContinuity = true;
        caster.tension += keyWordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
