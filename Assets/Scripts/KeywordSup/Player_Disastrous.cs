using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Disastrous : KeywordSup
{
    private void Awake()
    {
        keywordName = "재앙 같은";
        isPlayerKeyword = true;
        SetKeywordColor(Y);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.oneTimeReinforce, caster.charactorState.GetStateStack(StateType.oneTimeReinforce));
        caster.charactorState.AddState(StateType.weaken, debuffStack);
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
