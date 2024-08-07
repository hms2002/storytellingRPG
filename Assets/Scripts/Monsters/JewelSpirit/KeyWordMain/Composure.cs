using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Composure : KeywordMain
{
    private void Awake()
    {
        keywordName = "평정심";
        SetKeywordColor(Y);
        keywordTension = -40;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init(); 
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
