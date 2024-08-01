using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ori_Smash : KeywordMain
{
    [Header("랜덤 데미지 제어")]
    [SerializeField] private int maxRange = 9;
    [SerializeField] private int minRange = 6;
    private void Awake()
    {
        keywordName = "분쇄";

        SetKeywordColor(R);
        keywordTension = -10;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        keywordDamage = (int)Random.Range(minRange, maxRange + 1);
        caster.damage += keywordDamage;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}
