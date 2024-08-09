using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipwreckedGoblin_Lonely : KeywordSup
{
    private void Awake()
    {
        keywordName = "고독한";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateType.reduction, debuffStack);
        caster.tension += keywordTension;
    }
}
