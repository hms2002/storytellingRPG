using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Davythulhu_GlintEye : KeywordMain
{
    private void Awake()
    {
        keywordName = "안광";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateType.burn, target.charactorState.GetStateStack(StateType.fear) * debuffStack);
        target.charactorState.ResetState(StateType.fear);
        caster.tension += keywordTension;
    }
}
