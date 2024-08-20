using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meditation : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(Y);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.counterAttack, buffStack);
        caster.charactorState.AddState(StateType.weaken, debuffStack);
    }

    public override void Check(KeywordSup keywordSup)
    {

    }
}
