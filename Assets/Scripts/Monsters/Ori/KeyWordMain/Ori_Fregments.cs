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
        int minimumDamage = 5;
        int maximumDamage = 8;
        if(caster.charactorState.GetStateStack(StateType.ore) >= 5)
        {
            caster.charactorState.ReductionByValue(StateType.ore, 5);
            minimumDamage = 7;
            maximumDamage = 12;
        }
        keywordDamage = (int)Random.Range(minimumDamage, maximumDamage + 1);
        caster.dmgList.Add(keywordDamage);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}
