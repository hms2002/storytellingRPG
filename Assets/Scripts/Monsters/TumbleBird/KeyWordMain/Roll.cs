using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : KeywordMain
{
    [Header("구르기 랜덤 데미지 범위 제어")]
    [SerializeField] private int minDamage = 20;
    [SerializeField] private int maxDamage = 20;


    private void Awake()
    {
        keywordName = "구르기";
        SetKeywordColor(RED);
        keywordDamage = Random.Range(minDamage, maxDamage);
        debuffStack = 5;
        debuffType = "Burn";
        keywordTension = 41;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        caster.burnStack += debuffStack;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
