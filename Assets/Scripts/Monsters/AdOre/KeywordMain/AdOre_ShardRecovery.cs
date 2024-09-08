using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdOre_ShardRecovery : KeywordMain
{
    private void Awake()
    {
        keywordName = "파편 복구";
        SetKeywordColor(Y);
        keywordTension = 52;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.ore, buffStack);

        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}