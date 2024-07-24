using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : KeywordMain
{
    [Header("주먹 데미지")]
    [SerializeField] private int fistDamage = 1;


    private void Awake()
    {
        keywordName = "주먹";
        SetKeywordColor(R);
        keywordDamage = 1;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += fistDamage;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
