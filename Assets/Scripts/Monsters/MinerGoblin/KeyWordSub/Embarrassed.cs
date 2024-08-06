using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Embarrassed : KeywordSup
{
    [Header("당황한 키워드 데미지 랜덤 범위 제어")]
    [SerializeField] private int minDamage = 4;
    [SerializeField] private int maxDamage = 6;

    private void Awake()
    {
        keywordName = "당황한";
        SetKeywordColor(R);
        keywordTension = 6;
        keywordDamage = Random.Range(minDamage, maxDamage);
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
