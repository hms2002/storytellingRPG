using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApocalypseSorcerer_Curse : KeywordMain
{
    private void Awake()
    {
        keywordName = "저주";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        int usingManaStack = 0;
        int manaStack = caster.charactorState.GetStateStack(StateType.mana);
        if (manaStack > 0)
        {
            if (manaStack > 4)
                usingManaStack = 4;
            else
                usingManaStack = manaStack;
            caster.charactorState.ReductionByValue(StateType.mana, usingManaStack);

            target.charactorState.AddState(StateType.oneTimeReduction, debuffStack * usingManaStack);
            target.charactorState.AddState(StateType.venom, debuffStack * usingManaStack);
        }
        caster.tension += keywordTension;
    }
}
