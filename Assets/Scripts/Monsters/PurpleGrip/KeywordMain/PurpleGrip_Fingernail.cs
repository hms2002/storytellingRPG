using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleGrip_Fingernail : KeywordMain
{
    private void Awake()
    {
        keywordName = "손톱";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateType.addiction, debuffStack);
        caster.tension += keywordTension;
    }
}
