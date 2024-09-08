using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cracked : KeywordSup
{
    private void Awake()
    {
        keywordName = "깨어진";
        SetKeywordColor(Y);
        keywordTension = 20;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState
            (StateDatabase.stateDatabase.glassPragment, buffStack);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
