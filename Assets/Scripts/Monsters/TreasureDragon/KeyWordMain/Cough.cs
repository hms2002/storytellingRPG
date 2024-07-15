using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cough : KeywordMain
{
    [Header("랜덤 데미지 제어")]
    [SerializeField] private int maxRange = 30;
    [SerializeField] private int minRange = 20;

    private void Awake()
    {
        keywordName = "기침(브레스)";
        SetKeywordColor(RED);
        keywordDamage = Random.Range(minRange, maxRange);
        debuffStack = 5;
        debuffType = "Burn";
        keyWordTension = 41;
    }
    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        caster.burnStack += debuffStack;
        caster.tension += keyWordTension;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
