using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointed : KeywordSup
{
    TumbleBird tumbleBird;

    private void Awake()
    {
        keywordName = "뾰족한";
        SetKeywordColor(Y);
        keywordTension = -8;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        tumbleBird = caster as TumbleBird;
        caster.charactorState.AddState(StateDatabase.stateDatabase.oneTimeReinforce
            , tumbleBird.charactorState.BuffCount());
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
