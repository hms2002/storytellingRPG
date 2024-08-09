using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipwreckedGoblin_Shipwrecked : KeywordSup
{
    private void Awake()
    {
        keywordName = "조난당한";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateType.reduction, debuffStack);
        target.charactorState.AddState(StateType.weaken, debuffStack + 1);
        caster.tension += keywordTension;
    }
}
