using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Holy : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "성스러운";
        SetKeywordColor(B);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.protect, buffStack);
        caster.charactorState.AddState(StateType.oneTimeProtect, 6);
    }

    public override void Check(KeywordMain _keywordMain) { }
}
