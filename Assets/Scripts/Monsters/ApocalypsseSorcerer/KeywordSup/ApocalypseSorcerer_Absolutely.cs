using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApocalypseSorcerer_Absolutely : KeywordSup
{

    private void Awake()
    {
        keywordName = "절대적인";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        if (caster.charactorState.GetStateStack(StateType.mana) > 0)
        {
            caster.charactorState.ReductionByValue(StateType.mana, 1);
            target.charactorState.AddState(StateType.weaken, debuffStack);
            target.charactorState.AddState(StateType.reduction, debuffStack);
        }
        caster.tension += keywordTension;
    }
}
