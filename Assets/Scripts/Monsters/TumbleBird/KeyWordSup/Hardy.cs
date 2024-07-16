using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hardy : KeywordSup
{
    TumbleBird tumbleBird;

    
    private void Awake()
    {
        keywordName = "튼튼한";

        SetKeywordColor(BLUE);
        keyWordTension = -8;
    }

    public override void Execute(Actor caster, Actor target)
    {
        tumbleBird = caster as TumbleBird;
        caster.oneTimeProtect += tumbleBird.BuffCount();
        caster.tension += keyWordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
