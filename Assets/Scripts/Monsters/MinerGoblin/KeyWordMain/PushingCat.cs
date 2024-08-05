using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingCat : KeywordMain
{
    [Header("수레 밀치기 키워드 데미지 수치")]
    [SerializeField] private int damage = 30;

    private void Awake()
    {
        keywordName = "수레 밀치기";
        SetKeywordColor(R);
        keywordTension = 18;
        keywordDamage = damage;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += damage;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
