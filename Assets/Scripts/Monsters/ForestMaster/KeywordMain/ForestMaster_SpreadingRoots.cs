using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestMaster_SpreadingRoots : KeywordMain
{
    private void Awake()
    {
        keywordName = "뿌리 뻗치기";
        SetKeywordColor(R);
        keywordDamage = 5;
        keywordTension = 8;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        if (caster.charactorState.GetStateStack(StateType.multiplication) >= 2)
        {
            caster.charactorState.ReductionByValue(StateType.multiplication, 2);
            keywordDamage = 10;
        }
        caster.damage += keywordDamage;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
