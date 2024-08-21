using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sacred : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "신성한";
        SetKeywordColor(B);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.oneTimeProtect, buffStack);
    }

    public override void Check(KeywordMain _keywordMain) { }
}
