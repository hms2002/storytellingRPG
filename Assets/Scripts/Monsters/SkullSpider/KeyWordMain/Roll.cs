using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : KeywordMain
{
    TumbleBird tumbleBird;

    [Header("구르기 랜덤 데미지 범위 제어")]
    [SerializeField] private int minDamage = 5;
    [SerializeField] private int maxDamage = 8;


    private void Awake()
    {
        keywordName = "구르기";
        SetKeywordColor(RED);
        keywordDamage = Random.Range(minDamage, maxDamage);
        debuffStack = 5;
        keyWordTension = -6;
    }

    public override void Execute(Actor caster, Actor target)
    {
        tumbleBird = caster as TumbleBird;
        caster.damage += keywordDamage;
        tumbleBird.tumbleBirdsBuffList[Random.Range(0, tumbleBird.tumbleBirdsBuffList.Length)] += 5;
        caster.tension += keyWordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
