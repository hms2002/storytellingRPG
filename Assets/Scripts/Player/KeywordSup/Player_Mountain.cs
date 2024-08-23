using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Mountain : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "태산같은";
        SetKeywordColor(Y);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        if (caster.protect >= 3)
            caster.charactorState.AddState(StateType.reinforce, buffStack);
    }

    public override void Check(KeywordMain _keywordMain) { }
}
