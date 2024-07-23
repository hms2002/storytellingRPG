using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Roll : KeywordMain
{
    TumbleBird tumbleBird;

    [Header("구르기 랜덤 데미지 범위 제어")]
    [SerializeField] private int minDamage = 5;
    [SerializeField] private int maxDamage = 8;


    private void Awake()
    {
        keywordName = "구르기";
        SetKeywordColor(R);
        keywordDamage = UnityEngine.Random.Range(minDamage, maxDamage);
        debuffStack = 5;
        keywordTension = -6;
    }

    public override void Execute(Actor caster, Actor target)
    {
        tumbleBird = caster as TumbleBird;
        caster.damage += keywordDamage;
        
        int randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TumbleBird.TumbleBirdBuffList)).Length);
        TumbleBird.TumbleBirdBuffList buffEnum = (TumbleBird.TumbleBirdBuffList)Enum.GetValues(typeof(TumbleBird.TumbleBirdBuffList)).GetValue(randomIndex);
        caster.charactorState.AddState((StateType)buffEnum, 5);

        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
