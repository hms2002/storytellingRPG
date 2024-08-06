using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdShark_Bite : KeywordMain
{
    [Header("깨물기 키워드 데미지 수치")]
    [SerializeField] private int damage = 15;

    void Awake()
    {
        keywordName = "곡괭이";
        SetKeywordColor(R);
        keywordTension = 5;
        keywordDamage = damage;
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
