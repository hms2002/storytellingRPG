using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildOfLamp : KeywordMain
{
    private void Awake()
    {
        keywordName = "붉은 용의 송곳니";
        isPlayerKeyword = true;
        SetKeywordColor(Y);
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
