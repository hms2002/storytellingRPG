using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Lucky : KeywordSup
{
    [Header("운 좋은 키워드 데미지 랜덤 범위 제어")]
    [SerializeField] private int minDamage = 1;
    [SerializeField] private int maxDamage = 3;


    private void Awake()
    {
        keywordName = "운 좋은";
        SetKeywordColor(R);
        keywordDamage = Random.Range(minDamage, maxDamage);
        keywordTension = 4;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;

        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
