using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoorestMaster_Rising : KeywordSup
{
    private void Awake()
    {
        keywordName = "솟아난";

        SetKeywordColor(Y);
        keywordTension = -4;
        debuffStack = 3;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.multiplication, debuffStack);
        caster.tension += keywordTension;
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }
}
