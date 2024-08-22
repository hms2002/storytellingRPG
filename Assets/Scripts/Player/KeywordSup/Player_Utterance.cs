using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Utterance : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(Y);
        effectTarget = EffectTarget.target;
        keywordName = "발화의";
        effectType = EffectManager.EffectType.Flame;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        int bonus = 0;
        if (target.charactorState.GetStateStack(StateType.burn) > 0) bonus = 3;
        target.charactorState.AddState(StateDatabase.stateDatabase.burn, debuffStack + bonus);
    }

    public override void Check(KeywordMain _keywordMain) { }
}
