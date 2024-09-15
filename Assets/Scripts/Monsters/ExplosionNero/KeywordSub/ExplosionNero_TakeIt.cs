using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionNero_TakeIt : KeywordSup
{
    private void Awake()
    {
        keywordName = "받아라!";
        SetKeywordColor(Y);
        keywordTension = 10;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateDatabase.stateDatabase.oneTimeReinforce, buffStack);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
