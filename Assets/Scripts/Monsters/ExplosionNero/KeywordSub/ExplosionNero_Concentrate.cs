using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionNero_Concentrate : KeywordSup
{
    private void Awake()
    {
        keywordName = "집중하고...";
        SetKeywordColor(Y);
        keywordTension = -8;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateDatabase.stateDatabase.oneTimeReinforce, buffStack);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
