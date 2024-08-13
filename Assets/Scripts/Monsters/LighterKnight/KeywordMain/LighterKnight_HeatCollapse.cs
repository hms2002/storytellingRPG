using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterKnight_HeatCollapse : KeywordMain
{
    private void Awake()
    {
        keywordName = "열 붕괴";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {

        caster.charactorState.AddState(StateType.weaken, caster.charactorState.GetStateStack(StateType.burn));
        caster.charactorState.ResetState(StateType.burn);
        caster.tension += keywordTension;
    }
}
