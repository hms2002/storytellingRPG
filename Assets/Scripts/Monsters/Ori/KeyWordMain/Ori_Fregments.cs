using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ori_Fregments : KeywordMain
{
    private void Awake()
    {
        keywordName = "파편";

        SetKeywordColor(B);
        keywordTension = 28;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        int minimumDamage = 10;
        int maximumDamage = 15;
        if(caster.charactorState.GetStateStack(StateType.ore) >= 3)
        {
            caster.charactorState.ReductionByValue(StateType.ore, 3);
            minimumDamage = 13;
            maximumDamage = 18;
        }
        keywordDamage = (int)Random.Range(minimumDamage, maximumDamage + 1);
        caster.damage += keywordDamage;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}
