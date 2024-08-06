using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelSpirit_Shiny : KeywordSup
{
    TumbleBird tumbleBird;

    
    private void Awake()
    {
        keywordName = "연속";
        SetKeywordColor(Y);
        keywordTension = 10;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        tumbleBird = caster as TumbleBird;
        tumbleBird.isContinuity = true;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
