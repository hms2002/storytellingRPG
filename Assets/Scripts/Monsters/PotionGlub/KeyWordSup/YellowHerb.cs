using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowHerb : KeywordSup
{
    PotionGlub potionGlub;

    [Header("랜덤 포션수치 제어")]
    [SerializeField] private int maxRange = 4;
    [SerializeField] private int minRange = 2;

    private void Awake()
    {
        keywordName = "노란 약초";
        SetKeywordColor(BLUE);
        keywordTension = -10;
    }

    public override void Execute(Actor caster, Actor target)
    {
        potionGlub = caster as PotionGlub;
        potionGlub.potionNum += Random.Range(minRange, maxRange);
        potionGlub.ColorChecking();
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}