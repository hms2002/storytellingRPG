using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearPowerPotion : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(D);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.oneTimeProtect, buffStack);
        caster.protect += keywordProtect;
    }

    public override void Check(KeywordSup keywordSup)
    {

    }
}
