using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastKnightSword : KeywordMain
{
    [Header("속기사의 검 공격 횟수")]
    [SerializeField] private int attackCount = 2;


    private void Awake()
    {
        SetKeywordColor(R);
        Init();
    }
    
    public override void Execute(Actor caster, Actor target)
    {
        caster.repeatStack *= (attackCount);
        caster.damage += (keywordDamage);
    }

    public override void Check(KeywordSup keywordSup) { }
}
