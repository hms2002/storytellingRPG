using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApocalypseSorcerer_MagicShield : KeywordMain
{
    private void Awake()
    {
        keywordName = "마법 역장";

        SetKeywordColor(B);
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
            caster.protect += usingManaStack * buffStack;
        }
        caster.tension += keywordTension;
    }
}
