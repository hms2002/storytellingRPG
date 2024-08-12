using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleGrip_MentalBreakdown : KeywordMain
{
    private void Awake()
    {
        keywordName = "정신 붕괴";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateType.oneTimeReduction, debuffStack);
        caster.tension += keywordTension;
    }
}
