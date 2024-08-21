using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Expedited : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "신속한";
        SetKeywordColor(Y);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.evasion, buffStack);
    }

    public override void Check(KeywordMain _keywordMain) { }
}
