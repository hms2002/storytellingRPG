using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdShark_Sternutation : KeywordMain
{
    [Header("재채기 키워드 데미지")]
    [SerializeField] private int selfDamage;

    void Awake()
    {
        keywordName = "재채기";
        SetKeywordColor(R);
        keywordTension = -35;
        keywordDamage = selfDamage;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.damage += keywordDamage;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
