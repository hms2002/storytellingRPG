using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatteringTreasures : KeywordMain
{
    TrasureDragon trasureDragon;
    [Header("보물 뿌리기 키워드 랜덤 데미지 범위 제어")]
    [SerializeField] private int maxRange = 18;
    [SerializeField] private int minRange = 12;


    private void Awake()
    {
        keywordName = "보물 뿌리기";
        SetKeywordColor(RED);
        keywordDamage = Random.Range(minRange, maxRange);
        keyWordTension = 18;
    }

    public override void Execute(Actor caster, Actor target)
    {
        trasureDragon = caster as TrasureDragon;
        caster.damage += keywordDamage;
        trasureDragon.dragonsTrasure -= keywordDamage;
        trasureDragon.trasureDamage += keywordDamage;
        caster.tension += keyWordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
