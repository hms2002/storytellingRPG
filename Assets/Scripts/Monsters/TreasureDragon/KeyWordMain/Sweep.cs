using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweep : KeywordMain
{
    TrasureDragon trasureDragon;

    [Header("쓸어 담기 키워드 랜덤 데미지 범위 제어")]
    [SerializeField] private int maxRange = 18;
    [SerializeField] private int minRange = 15;


    private void Awake()
    {
        keywordName = "쓸어 담기";
        SetKeywordColor(RED);
        keywordDamage = Random.Range(minRange, maxRange);
        keywordTension = 18;
    }

    public override void Execute(Actor caster, Actor target)
    {
        trasureDragon = caster as TrasureDragon;
        trasureDragon.damage += keywordDamage;
        trasureDragon.dragonsTrasure -= 10;
        trasureDragon.trasureDamage += keywordDamage;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
