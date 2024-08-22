using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BangBang : KeywordMain
{
    private void Awake()
    {
        keywordName = "탕탕!!";
        SetKeywordColor(R);
        Init();
    }
        
    public override void Execute(Actor caster, Actor target)
    {
        caster.tension += keywordTension;
        caster.dmgList.Add(keywordDamage);
        caster.dmgList.Add(keywordDamage);
        caster.charactorState.ReductionByValue(StateType.ammunition, buffStack);
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void CanUseCheck(Actor caster, Actor target)
    {
        if (caster.charactorState.GetStateStack(StateType.ammunition) < 2)
        {
            isCanUse = false;
        }
    }
}
