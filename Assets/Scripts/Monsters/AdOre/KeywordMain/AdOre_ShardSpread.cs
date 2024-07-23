using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdOre_ShardSpread : KeywordMain
{
    [Header("광석 스택 감소 수치 제어")]
    [SerializeField] int amountOfDecrease = 6;

    private void Awake()
    {
        keywordName = "파편 확산";
        //SetKeywordColor(R);
        keywordDamage = 32;
        keywordTension = 32;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;

        caster.charactorState.ReductionByValue(StateType.ore, amountOfDecrease);

        target.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}