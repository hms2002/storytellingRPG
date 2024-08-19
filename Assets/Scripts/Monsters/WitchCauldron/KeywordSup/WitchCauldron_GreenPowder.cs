using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchCauldron_GreenPowder : KeywordSup
{
    WitchCauldron witchCauldron;

    [Header("랜덤 포션수치 제어")]
    [SerializeField] private int maxRange = 3;
    [SerializeField] private int minRange = -1;

    private void Awake()
    {
        keywordName = "초록 가루";
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
    public override void Check(KeywordMain _keywordMain)
    {

    }
}
