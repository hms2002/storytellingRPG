using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointed : KeywordSup
{
    TumbleBird tumbleBird;

    
    private void Awake()
    {
        keywordName = "뾰족한";
        SetKeywordColor(R);
        keywordTension = -8;
    }

    public override void Execute(Actor caster, Actor target)
    {
        tumbleBird = caster as TumbleBird;
        caster.charactorState.AddState(StateDatabase.stateDatabase.oneTimeReinforce
            , tumbleBird.charactorState.BuffCount());
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
