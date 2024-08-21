using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_LiedDagger : KeywordMain
{
    private void Awake()
    {
        keywordName = "거짓된 단검";
        isPlayerKeyword = true;
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        caster.charactorState.StackReductionProtect(StateType.oneTimeReinforce);
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
