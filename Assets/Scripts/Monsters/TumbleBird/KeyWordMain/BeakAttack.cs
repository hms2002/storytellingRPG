using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeakAttack : KeywordMain
{
    [Header("랜덤 데미지 수치 제어")]
    [SerializeField] private int maxRange = 12;
    [SerializeField] private int minRange = 8;

    private void Awake()
    {
        keywordName = "부리 공격";
        SetKeywordColor(RED);
        keywordDamage = Random.Range(minRange, maxRange);
        keyWordTension = 18;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage = keywordDamage;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
