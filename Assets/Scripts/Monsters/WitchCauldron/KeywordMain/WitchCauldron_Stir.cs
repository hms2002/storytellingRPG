using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchCauldron_Stir : KeywordMain
{
    WitchCauldron witchCauldron;

    [Header("랜덤 포션수치 제어")]
    [SerializeField] private int maxRange = 2;
    [SerializeField] private int minRange = -2;

    private void Awake()
    {
        keywordName = "휘젓기";
        SetKeywordColor(Y);
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        witchCauldron = caster as WitchCauldron;
        witchCauldron.potionNum += Random.Range(minRange, maxRange + 1);
        witchCauldron.ColorChecking();
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
