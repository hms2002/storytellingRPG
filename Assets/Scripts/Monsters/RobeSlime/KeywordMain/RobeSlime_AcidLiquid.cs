using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobeSlime_AcidLiquid : KeywordMain
{
    private void Awake()
    {
        keywordName = "산성액";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    int MAX_COST = 2;
    public override void Execute(Actor caster, Actor target)
    {
        int stack = caster.charactorState.GetStateStack(StateType.mana);
        if (stack > MAX_COST) stack = MAX_COST;
        target.charactorState.AddState(StateType.addiction, stack);
        caster.charactorState.ReductionByValue(StateType.mana, stack);
        caster.tension += keywordTension;
    }
}
