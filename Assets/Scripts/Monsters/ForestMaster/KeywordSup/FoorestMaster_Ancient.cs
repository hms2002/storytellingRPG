using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoorestMaster_Ancient : KeywordSup
{
    private void Awake()
    {
        keywordName = "고대의";

        SetKeywordColor(Y);
        keywordTension = -6;
        debuffStack = 2;
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
