using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ProtectivePosture : KeywordMain
{
    private void Awake()
    {
        keywordName = "처형자의 칼날";
        isPlayerKeyword = true;
        SetKeywordColor(R);
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
