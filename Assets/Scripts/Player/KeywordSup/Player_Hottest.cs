using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hottest : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(R);
        effectTarget = EffectTarget.target;
        keywordName = "화끈한";
        effectType = EffectManager.EffectType.Flame;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        int stack = target.charactorState.GetStateStack(StateType.burn) / 3;
        caster.dmgList.Plus(stack);
    }

    public override void Check(KeywordMain _keywordMain) { }
}
