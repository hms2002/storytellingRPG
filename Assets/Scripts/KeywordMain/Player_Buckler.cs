using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Buckler : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "버클러";
        SetKeywordColor(B);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.oneTimeProtect, buffStack);
        caster.charactorState.AddState(StateType.counterAttack, 2);
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
