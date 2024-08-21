using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hidden : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "숨겨진";
        SetKeywordColor(Y);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.counterAttack, buffStack);
    }

    public override void Check(KeywordMain _keywordMain) { }
}
