using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : KeywordSup
{
    private void Awake()
    {
        keywordName = "끈적이는";
        SetKeywordColor(Y);
        keywordTension = 5;
        debuffStack = 1;
        effectTarget = EffectTarget.target;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateDatabase.stateDatabase.addiction, debuffStack);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
