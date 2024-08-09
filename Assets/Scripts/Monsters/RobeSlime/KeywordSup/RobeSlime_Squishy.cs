using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobeSlime_Squishy : KeywordSup
{
    private void Awake()
    {
        keywordName = "질척이는";

        SetKeywordColor(B);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.oneTimeProtect, buffStack);
        caster.tension += keywordTension;
    }
}
