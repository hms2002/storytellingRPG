using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumpy : KeywordSup
{
    TumbleBird tumbleBird;


    private void Awake()
    {
        keywordName = "엉망진창";
        SetKeywordColor(B);
        keywordTension = 12;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        tumbleBird = caster as TumbleBird;
        caster.protect += tumbleBird.charactorState.BuffCount();
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
