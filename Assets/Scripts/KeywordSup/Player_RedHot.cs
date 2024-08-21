using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RedHot : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(Y);
        effectTarget = EffectTarget.target;
        keywordName = "적열의";
        effectType = EffectManager.EffectType.Flame;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        int stack = target.charactorState.GetStateStack(StateType.burn) / 5;
        caster.charactorState.AddState(StateType.reinforce, stack);
    }

    public override void Check(KeywordMain _keywordMain) { }
}
