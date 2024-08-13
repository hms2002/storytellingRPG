using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Concentrate : KeywordSup
{
    private void Awake()
    {
        keywordName = "집중하고...";
        SetKeywordColor(Y);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.oneTimeReinforce, buffStack);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
