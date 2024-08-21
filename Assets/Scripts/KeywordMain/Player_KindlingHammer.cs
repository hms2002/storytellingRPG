using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_KindlingHammer : KeywordMain
{
    private void Awake()
    {
        keywordName = "불쏘시개 해머";
        isPlayerKeyword = true;
        SetKeywordColor(Y);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateType.burn, target.charactorState.GetStateStack(StateType.burn));
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
