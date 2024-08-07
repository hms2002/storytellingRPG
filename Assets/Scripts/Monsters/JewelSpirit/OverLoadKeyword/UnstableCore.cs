using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstableCore : KeywordMain
{
    [Header("랜덤 데미지 수치 제어")]
    [SerializeField] private int maxRange = 35;
    [SerializeField] private int minRange = 20;

    private void Awake()
    {
        keywordName = "불안정 코어";
        SetKeywordColor(R);
        keywordDamage = Random.Range(minRange, maxRange);
        keywordProtect = 20;
        keywordTension = -10;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.tension += keywordTension;
        caster.protect -= keywordProtect;
        caster.damage = keywordDamage;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
