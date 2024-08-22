using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evil : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(Y);
        effectTarget = EffectTarget.target;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    { 
        target.charactorState.AddState(StateDatabase.stateDatabase.weaken, debuffStack);
    }

    public override void Check(KeywordMain _keywordMain) { }
}
