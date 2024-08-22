using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BigShield : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "대형 방패";
        SetKeywordColor(B);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;
        caster.charactorState.AddState(StateType.reduction, debuffStack);
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
