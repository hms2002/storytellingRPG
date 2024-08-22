using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastKnightSword : KeywordMain
{
    [Header("속기사의 검 공격 횟수")]
    [SerializeField] private int attackCount = 2;


    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(R);
        Init();
    }
    
    public override void Execute(Actor caster, Actor target)
    {
        caster.dmgList.Add(keywordDamage);
        caster.dmgList.Add(keywordDamage);
    }

    public override void Check(KeywordSup keywordSup) { }
}
