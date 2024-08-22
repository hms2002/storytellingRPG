using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Thrilled : KeywordSup
{
    private void Awake()
    {
        keywordName = "짜릿한";
        isPlayerKeyword = true;
        SetKeywordColor(Y);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.oneTimeReinforce, caster.charactorState.GetStateStack(StateType.weaken) / 2);
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
