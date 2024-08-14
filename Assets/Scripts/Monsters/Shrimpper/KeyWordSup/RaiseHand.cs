using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseHand : KeywordSup
{
    private void Awake()
    {
        keywordName = "손들어!";
        SetKeywordColor(Y);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.ammunition, buffStack);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordSup)
    {
    }
}
