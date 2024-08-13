using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterKnight_Burning : KeywordSup
{
    private void Awake()
    {
        keywordName = "타오르는";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.burn, debuffStack);
        target.charactorState.AddState(StateType.burn, debuffStack);
        caster.tension += keywordTension;
    }
}
