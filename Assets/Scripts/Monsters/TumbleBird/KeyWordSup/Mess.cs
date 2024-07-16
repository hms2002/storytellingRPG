using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mess : KeywordSup
{
    TumbleBird tumbleBird;


    private void Awake()
    {
        keywordName = "엉망진창";
        SetKeywordColor(BLUE);
        keywordTension = 12;
    }
    
    public override void Execute(Actor caster, Actor target)
    {
        tumbleBird = caster as TumbleBird;
        caster.protect += tumbleBird.BuffCount();
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
