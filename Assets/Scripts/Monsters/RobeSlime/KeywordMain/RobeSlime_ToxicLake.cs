using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobeSlime_ToxicLake : KeywordMain
{
    private void Awake()
    {
        keywordName = "맹독성 호수";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }
    int MAX_COST = 4;
    int ONE_TIME_COST = 2;
    public override void Execute(Actor caster, Actor target)
    {
        int stack = caster.charactorState.GetStateStack(StateType.mana);
        if (stack > MAX_COST) stack = MAX_COST;
        stack /= ONE_TIME_COST;
        target.charactorState.AddState(StateType.addiction, stack * ONE_TIME_COST);
        caster.charactorState.ReductionByValue(StateType.mana, stack * ONE_TIME_COST);
        caster.tension += keywordTension;
    }
}
