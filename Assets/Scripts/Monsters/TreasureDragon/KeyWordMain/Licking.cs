using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liking : KeywordMain
{
    [Header("랜덤 데미지 제어")]
    [SerializeField] private int maxRange = 10;
    [SerializeField] private int minRange = 6;

    private void Awake()
    {
        keywordName = "핥기";
        SetKeywordColor(R);
        keywordDamage = Random.Range(minRange, maxRange);
        keywordTension = -9;
        Init();
    }
    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        caster.tension += keywordTension;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
