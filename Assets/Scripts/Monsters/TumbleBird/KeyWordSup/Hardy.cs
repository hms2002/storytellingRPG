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
        keywordTension = -8;
    }

    public override void Execute(Actor caster, Actor target)
    {
        tumbleBird = caster as TumbleBird;
        caster.charactorState.AddState(StateDatabase.stateDatabase.oneTimeProtect, tumbleBird.charactorState.BuffCount());
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
