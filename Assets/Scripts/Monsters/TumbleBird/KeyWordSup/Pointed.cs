using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointed : KeywordSup
{
    TumbleBird tumbleBird;
    private void Awake()
    {
        keywordName = "뾰족한";

        SetKeywordColor(RED);
        keyWordTension = -8;
    }

    public override void Execute(Actor caster, Actor target)
    {
        tumbleBird = caster as TumbleBird;
        caster.oneTimeReinforce += tumbleBird.BuffCount();
        caster.tension += keyWordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
