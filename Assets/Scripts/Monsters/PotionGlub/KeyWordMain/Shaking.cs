using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking : KeywordMain
{
    PotionGlub potionGlub;

    [Header("랜덤 포션수치 제어")]
    [SerializeField] private int maxRange = 3;
    [SerializeField] private int minRange = -3;

    private void Awake()
    {
        keywordName = "셰이킹";
        SetKeywordColor(BLUE);
        keywordTension = -5;
    }

    public override void Execute(Actor caster, Actor target)
    {
        potionGlub = caster as PotionGlub;
        potionGlub.potionNum = Random.Range(minRange, maxRange);
        potionGlub.ColorChecking();
        caster.tension += keywordTension;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
