using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionNero_Yay : KeywordSup
{
    private void Awake()
    {
        keywordName = "이얍!";
        SetKeywordColor(Y);
        keywordTension = -7;
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
