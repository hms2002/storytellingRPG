using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobeSlime_Mysterious : KeywordSup
{
    private void Awake()
    {
        keywordName = "미스테리한";

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
