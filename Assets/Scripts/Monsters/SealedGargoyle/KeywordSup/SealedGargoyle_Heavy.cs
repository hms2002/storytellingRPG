using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealedGargoyle_Heavy : KeywordSup
{
    private void Awake()
    {
        keywordName = "무거운";

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
