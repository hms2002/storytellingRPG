using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Unexpected : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "기습적인";
        SetKeywordColor(Y);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateType.weaken, debuffStack);
    }

    public override void Check(KeywordMain _keywordMain) { }
}
