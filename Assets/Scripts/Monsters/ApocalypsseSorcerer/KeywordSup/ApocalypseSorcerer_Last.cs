using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApocalypseSorcerer_Last : KeywordSup
{

    private void Awake()
    {
        keywordName = "최후의";

        SetKeywordColor(R);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        if(caster.charactorState.GetStateStack(StateType.mana) > 0)
        {
            caster.charactorState.ReductionByValue(StateType.mana, 1);
            caster.charactorState.AddState(StateType.reinforce, buffStack);
        }
        caster.tension += keywordTension;
    }
}
