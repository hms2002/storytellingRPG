using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestMaster_ForestFury : KeywordMain
{
    private void Awake()
    {
        keywordName = "숲의 분노";
        SetKeywordColor(R);
        keywordDamage = 7;
        keywordTension = 5;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage * caster.charactorState.GetStateStack(StateType.multiplication);
        caster.tension += keywordTension;
        caster.charactorState.ResetState(StateType.multiplication);
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
    public override void CanUseCheck(Actor caster, Actor target)
    {

        if (caster.charactorState.GetStateStack(StateType.multiplication) >= 2)
            isCanUse = true;
        else
            isCanUse = false;
    }
}
