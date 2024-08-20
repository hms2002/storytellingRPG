using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reorganize : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(Y);
        Init();
    }

    public override void Execute(Actor caster, Actor target) 
    {
        caster.protect += keywordProtect;
        caster.charactorState.AddState(StateType.reinforce, buffStack);
    }

    public override void Check(KeywordSup keywordSup)
    {

    }
}
