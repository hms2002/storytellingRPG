using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchCauldron_SpellUP : KeywordMain
{
    WitchCauldron witchCauldron;


    private void Awake()
    {
        keywordName = "마법 주문";
        SetKeywordColor(Y);
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        witchCauldron = caster as WitchCauldron;
        witchCauldron.isSpellUp = true;
        caster.tension += keywordTension;
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }
}
