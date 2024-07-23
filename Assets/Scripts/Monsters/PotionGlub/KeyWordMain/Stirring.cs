using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stirring : KeywordMain
{
    PotionGlub potionGlub;

    [Header("랜덤 포션수치 제어")]
    [SerializeField] private int maxRange = 3;
    [SerializeField] private int minRange = 1;

    private void Awake()
    {
        keywordName = "스터링";
        SetKeywordColor(B);
        keywordTension = -8;
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

