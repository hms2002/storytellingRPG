using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Alerted : KeywordSup
{
    private void Awake()
    {
        keywordName = "경계된";
        isPlayerKeyword = true;
        SetKeywordColor(B);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += caster.charactorState.GetStateStack(StateType.weaken) * 2;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
