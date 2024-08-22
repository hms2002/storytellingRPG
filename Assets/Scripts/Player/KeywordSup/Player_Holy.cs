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
        caster.protect += keywordProtect;
        caster.charactorState.AddState(StateType.oneTimeProtect, buffStack);
    }

    public override void Check(KeywordMain _keywordMain) { }
}
