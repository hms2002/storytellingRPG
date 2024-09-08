using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowQuality : KeywordSup
{
    private void Awake()
    {
        keywordName = "저품질";
        SetKeywordColor(Y);
        keywordTension = -28;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateDatabase.stateDatabase.weaken, debuffStack);
 
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
