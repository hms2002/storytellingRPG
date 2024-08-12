using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleGrip_Flame : KeywordMain
{
    private void Awake()
    {
        keywordName = "불꽃";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateType.burn, debuffStack);
        caster.tension += keywordTension;
    }
}
