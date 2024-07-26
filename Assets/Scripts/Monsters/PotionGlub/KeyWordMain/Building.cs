using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : KeywordMain
{
    PotionGlub potionGlub;

    [Header("랜덤 포션수치 제어")]
    [SerializeField] private int maxRange = 1;
    [SerializeField] private int minRange = -2;

    private void Awake()
    {
        keywordName = "빌딩";
        SetKeywordColor(Y);
        keywordTension = -4;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        potionGlub = caster as PotionGlub;
        potionGlub.potionNum += Random.Range(minRange, maxRange);
        potionGlub.ColorChecking();
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}
