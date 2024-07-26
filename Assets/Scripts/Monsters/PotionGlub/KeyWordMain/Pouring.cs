using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pouring : KeywordMain
{
    PotionGlub potionGlub;
    private void Awake()
    {
        SetKeywordColor(Y);
        keywordTension = 10;
        effectTarget = EffectTarget.target;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        potionGlub = caster as PotionGlub;
        potionGlub.PotionHitted(target);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}