using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobeSlime_ManaRecovery : KeywordMain
{
    private void Awake()
    {
        keywordName = "마나 회복";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }
    int MAX_COST = 4;
    int ONE_TIME_COST = 2;
    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.mana, buffStack);
        caster.tension += keywordTension;
    }
}
