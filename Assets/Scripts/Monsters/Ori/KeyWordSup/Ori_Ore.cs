using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ori_Ore : KeywordSup
{ 
    private void Awake()
    {
        keywordName = "광석";

        SetKeywordColor(Y);
        keywordTension = 12;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.ReductionByValue(StateType.ore, 1);
        caster.charactorState.AddState(StateType.oneTimeReinforce, 3);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
