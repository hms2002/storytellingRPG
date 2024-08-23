using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ProtectivePosture : KeywordMain
{
    private void Awake()
    {
        keywordName = "보호 태세";
        isPlayerKeyword = true;
        SetKeywordColor(B);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;
        caster.protect += caster.charactorState.GetStateStack(StateType.weaken) * 2;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
