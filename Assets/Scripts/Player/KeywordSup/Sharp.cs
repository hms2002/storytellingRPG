using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharp : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "날카로운";
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.reinforce, buffStack);
    }

    public override void Check(KeywordMain _keywordMain) { }
}
