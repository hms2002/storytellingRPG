using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Processed : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(Y);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.reinforce, buffStack);
    }

    public override void Check(KeywordMain keywordMain)
    {

    }
}
