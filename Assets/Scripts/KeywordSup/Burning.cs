using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burning : KeywordSup
{
    private void Awake()
    {
        SetKeywordColor(Y);
        effectTarget = EffectTarget.target;
        effectType = EffectManager.EffectType.Flame;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateDatabase.stateDatabase.burn, debuffStack);
    }

    public override void Check(KeywordMain _keywordMain) { }
}
