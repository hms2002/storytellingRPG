using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyShot : KeywordMain
{
    PotionGlub potionGlub;
    private void Awake()
    {
        keywordName = "젤리샷";
        SetKeywordColor(Y);
        keywordTension = 20;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        potionGlub = caster as PotionGlub;
        potionGlub.isJellyShot = true;
        caster.tension += keywordTension;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
