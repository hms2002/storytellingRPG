using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Thoughtful : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "심사숙고한";
        SetKeywordColor(B);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.oneTimeProtect, buffStack);
        caster.charactorState.AddState(StateType.weaken, debuffStack);
    }

    public override void Check(KeywordMain _keywordMain) { }
}
