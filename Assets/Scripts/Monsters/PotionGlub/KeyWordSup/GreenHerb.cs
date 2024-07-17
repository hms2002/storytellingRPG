using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenHerb : KeywordSup
{
    PotionGlub potionGlub;
    [Header("랜덤 포션수치 제어")]
    [SerializeField] private int maxRange = 3;
    [SerializeField] private int minRange = 2;
    private void Awake()
    {
        keywordName = "초록 약초";
        SetKeywordColor(BLUE);
        keywordTension = -7;
    }

    public override void Execute(Actor caster, Actor target)
    {
        potionGlub = caster as PotionGlub;
        potionGlub.potionNum += Random.Range(minRange,maxRange);
        potionGlub.ColorChecking();
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
