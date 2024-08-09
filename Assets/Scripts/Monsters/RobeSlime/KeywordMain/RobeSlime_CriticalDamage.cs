using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobeSlime_CriticalDamage : KeywordMain
{
    private void Awake()
    {
        keywordName = "치명적 손상";

        SetKeywordColor(R);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    int COST = 15;
    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.ReductionByValue(StateType.mana, COST);
        target.Damaged(keywordDamage, caster, true);
        caster.tension += keywordTension;
    }

    public override void CanUseCheck(Actor caster, Actor target)
    {
        int stack = caster.charactorState.GetStateStack(StateType.mana);
        if (stack >= COST)
            isCanUse = true;
        else
            isCanUse = false;
    }
}
