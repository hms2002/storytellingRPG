using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobeSlime_Melted : KeywordSup
{
    private void Awake()
    {
        keywordName = "녹아내린";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateType.addiction, debuffStack);
        target.charactorState.AddState(StateType.venom, debuffStack + 1);
        caster.tension += keywordTension;
    }
}
