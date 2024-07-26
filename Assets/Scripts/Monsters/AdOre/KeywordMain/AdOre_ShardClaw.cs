using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdOre_ShardClaw : KeywordMain
{
    [Header("파편 발톱 데미지 범위 제어")]
    [SerializeField] int amountOfMinDamage = 8;
    [SerializeField] int amountOfMaxDamage = 12;

    private void Awake()
    {
        keywordName = "파편 발톱";
        SetKeywordColor(R);
        keywordDamage = Random.Range(amountOfMinDamage, amountOfMaxDamage);
        keywordTension = -12;
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
