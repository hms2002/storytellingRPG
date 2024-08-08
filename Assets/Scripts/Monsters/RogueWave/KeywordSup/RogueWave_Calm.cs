using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueWave_Calm : KeywordSup
{
    private void Awake()
    {
        keywordName = "잔잔한";

        SetKeywordColor(Y);
        keywordTension = -30;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.reduction, 5);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordSup)
    {

    }
}
