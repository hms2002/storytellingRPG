using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearAtTheStake : KeywordMain
{
    private void Awake()
    {
        keywordName = "화형의 창 날";
        SetKeywordColor(RED);
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.Damaged(caster, target.burnStack , DamageType.Beat);
        caster.damage += target.burnStack;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
