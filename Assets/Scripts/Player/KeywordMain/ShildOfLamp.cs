using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildOfLamp : KeywordMain
{
    private void Awake()
    {
        keywordName = "등불의 방패";
        isPlayerKeyword = true;
        SetKeywordColor(B);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateType.burn, buffStack);
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
