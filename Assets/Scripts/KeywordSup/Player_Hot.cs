using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hot : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(B);
        effectTarget = EffectTarget.target;
        keywordName = "뜨거운";
        effectType = EffectManager.EffectType.Flame;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        int stack = target.charactorState.GetStateStack(StateType.burn) / 2;
        caster.protect += stack;
    }

    public override void Check(KeywordMain _keywordMain) { }
}
