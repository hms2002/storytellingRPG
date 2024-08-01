using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ori_Pointed : KeywordSup
{
    [Header("랜덤 데미지 제어")]
    [SerializeField] private int maxRange = 3;
    [SerializeField] private int minRange = 1;
    private void Awake()
    {
        keywordName = "뾰족한";

        SetKeywordColor(R);
        keywordTension = -8;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        keywordDamage = Random.Range(minRange, maxRange + 1);
        caster.damage += keywordDamage;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
