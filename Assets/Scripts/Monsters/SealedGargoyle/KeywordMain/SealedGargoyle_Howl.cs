using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealedGargoyle_Howl : KeywordMain
{
    private void Awake()
    {
        keywordName = "울부짖기";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateType.reduction, debuffStack);
        target.charactorState.AddState(StateType.oneTimeReduction, debuffStack + 2);
        caster.tension += keywordTension;
    }
}
