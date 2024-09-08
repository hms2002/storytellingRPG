using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdOre_ShardOverload : KeywordMain
{
    private void Awake()
    {
        keywordName = "파편 폭주";
        SetKeywordColor(Y);
        keywordTension = 26;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.oneTimeReinforce, buffStack);

        caster.charactorState.ReductionByValue(StateType.ore, debuffStack);

        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}