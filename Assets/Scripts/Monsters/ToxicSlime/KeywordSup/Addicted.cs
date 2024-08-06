using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addicted : KeywordSup
{
    private void Awake()
    {
        keywordName = "중독된";
        SetKeywordColor(Y);
        keywordTension = 15;
        effectTarget = EffectTarget.target;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.StackDamageMultiplication(StateType.addiction);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void CanUseCheck(Actor caster, Actor target)
    {
        if (target.charactorState.GetStateStack(StateType.addiction) <= 0)
        {
            isCanUse = false;
        }
        else
        {
            isCanUse = true;
        }
    }
}
