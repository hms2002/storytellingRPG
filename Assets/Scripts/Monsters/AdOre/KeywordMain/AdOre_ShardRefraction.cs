using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdOre_ShardRefraction : KeywordMain
{

    private void Awake()
    {
        keywordName = "파편 굴절";
        SetKeywordColor(Y);
        keywordTension = -8;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.ReductionByValue(StateType.weaken, 3);

        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
