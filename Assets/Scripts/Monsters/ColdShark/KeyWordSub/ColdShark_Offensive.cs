using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdShark_Offensive : KeywordSup
{
    [Header("공격적인 키워드 데미지 수치")]
    [SerializeField] private int damage = 10;

    private void Awake()
    {
        keywordName = "공격적인";
        SetKeywordColor(R);
        keywordTension = 7;
        keywordDamage = damage;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        caster.tension =+ keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
