using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : KeywordMain
{
    private void Awake()
    {
        keywordName = "장전";
        SetKeywordColor(Y);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.ammunition, buffStack);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
