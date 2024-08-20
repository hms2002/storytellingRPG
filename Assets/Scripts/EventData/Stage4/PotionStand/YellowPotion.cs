using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowPotion : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(D);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.oneTimeReinforce, buffStack);
        caster.charactorState.AddState(StateType.weaken, debuffStack);
    }

    public override void Check(KeywordSup keywordSup)
    {

    }
}
