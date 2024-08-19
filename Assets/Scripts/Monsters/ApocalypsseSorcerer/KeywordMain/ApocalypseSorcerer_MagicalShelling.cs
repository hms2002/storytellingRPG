using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApocalypseSorcerer_MagicalShelling : KeywordMain
{
    private void Awake()
    {
        keywordName = "마법 포격";

        SetKeywordColor(R);
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
            if (manaStack > 8)
                usingManaStack = 8;
            else
                usingManaStack = manaStack;
            caster.charactorState.ReductionByValue(StateType.mana, usingManaStack);

            for (int i = 0; i < usingManaStack; i++)
                target.Damaged(caster, keywordDamage);
        }
        caster.tension += keywordTension;
    }
}
