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
        keywordTension = 18;
    }

    public override void Execute(Actor caster, Actor target)
    {
        trasureDragon = caster as TrasureDragon;
        caster.damage += keywordDamage;
        trasureDragon.charactorState.ReductionByValue(StateType.treasureOfDragon, keywordDamage);
        trasureDragon.trasureDamage += keywordDamage;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
