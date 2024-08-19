using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApocalypseSorcerer_Ceremony : KeywordMain
{
    private void Awake()
    {
        keywordName = "의식";

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
            if (manaStack > 2)
                usingManaStack = 2;
            else
                usingManaStack = manaStack;
            caster.charactorState.ReductionByValue(StateType.mana, usingManaStack);
            caster.charactorState.AddState(StateType.end, usingManaStack);
            caster.charactorState.AddState(StateType.weaken, usingManaStack * debuffStack);
        }
        caster.tension += keywordTension;
    }
}
