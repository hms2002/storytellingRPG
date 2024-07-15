using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastKnightSword : KeywordMain
{
    [Header("속기사의 검 공격 횟수")]
    [SerializeField] private int attackNum = 2;

    private void Awake()
    {
        keywordName = "속기사의 검";
        SetKeywordColor(RED);
        keywordDamage = 2;
    }
    
    public override void Execute(Actor caster, Actor target)
    {
        caster.repeatStack *= (attackNum);
        caster.damage += (keywordDamage);
    }

    public override void Check(KeywordSup keywordSup)
    {
    }
}
