using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApocalypseSorcerer_EnergyExtract : KeywordMain
{
    private void Awake()
    {
        keywordName = "에너지 추출";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.ReductionByValue(StateType.end, 2);
        caster.charactorState.AddState(StateType.mana, buffStack);
        caster.tension += keywordTension;
    }

    public override void CanUseCheck(Actor caster, Actor target)
    {   
        if(caster.charactorState.GetStateStack(StateType.end) > 2)
        {
            isCanUse = false;
        }
        
    }
}
