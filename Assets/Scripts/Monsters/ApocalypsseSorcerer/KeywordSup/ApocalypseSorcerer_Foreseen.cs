using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApocalypseSorcerer_Foreseen : KeywordSup
{

    private void Awake()
    {
        keywordName = "예견된";

        SetKeywordColor(B);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        if (caster.charactorState.GetStateStack(StateType.mana) > 1)
        {
            caster.charactorState.ReductionByValue(StateType.mana, 2);
            caster.charactorState.AddState(StateType.oneTimeProtect, buffStack);
        }
        caster.tension += keywordTension;
    }
}
