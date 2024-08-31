using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RoundedSolarShield : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "원형 태양 방패";
        SetKeywordColor(B);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;
        caster.charactorState.AddState(StateType.evasion, buffStack);
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
