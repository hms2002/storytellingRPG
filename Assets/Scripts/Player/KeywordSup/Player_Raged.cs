using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Raged : KeywordSup
{
    private void Awake()
    {
        keywordName = "몰아치는";
        isPlayerKeyword = true;
        SetKeywordColor(Y);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.oneTimeReinforce, buffStack);
        caster.charactorState.AddState(StateType.weaken, debuffStack);
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
