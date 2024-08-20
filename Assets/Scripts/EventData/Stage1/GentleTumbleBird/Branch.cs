using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(Y);
        Init();
    }

    public override void Execute(Actor caster, Actor target) 
    {
        caster.charactorState.AddState(StateType.oneTimeReinforce, buffStack);
    }

    public override void Check(KeywordSup keywordSup)
    {

    }
}
