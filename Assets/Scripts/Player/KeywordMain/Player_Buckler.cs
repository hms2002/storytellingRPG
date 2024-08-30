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
        caster.protect += keywordProtect;
        caster.charactorState.AddState(StateType.counterAttack, buffStack);
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
