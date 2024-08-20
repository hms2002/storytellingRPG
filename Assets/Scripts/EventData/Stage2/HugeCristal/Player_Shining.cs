using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shining : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(Y);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateType.oneTimeReduction, debuffStack);
    }

    public override void Check(KeywordMain keywordMain)
    {

    }
}
