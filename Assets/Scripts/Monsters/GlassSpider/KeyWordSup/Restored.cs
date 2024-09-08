using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restored : KeywordSup
{
    private void Awake()
    {
        keywordName = "수복된";
        SetKeywordColor(Y);
        keywordTension = -20;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.ReductionByValue(StateType.glassPragment, buffStack);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
