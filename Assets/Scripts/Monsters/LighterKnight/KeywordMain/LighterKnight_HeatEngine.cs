using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterKnight_HeatEngine : KeywordMain
{
    private void Awake()
    {
        keywordName = "열기관";

        SetKeywordColor(B);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += caster.charactorState.GetStateStack(StateType.burn);
        caster.tension += keywordTension;
    }
}
