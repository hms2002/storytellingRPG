using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Davythulhu_Maddening : KeywordSup
{

    private void Awake()
    {
        keywordName = "광기어린";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateType.weaken, debuffStack);
        caster.charactorState.AddState(StateType.weaken, debuffStack);
        caster.tension += keywordTension;
    }
}
