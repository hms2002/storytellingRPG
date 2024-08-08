using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnatchFlyingFish_Bouncing : KeywordSup
{
    private void Awake()
    {
        keywordName = "튀어오르는";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.evasion, 1);
        caster.tension += keywordTension;
    }
}
