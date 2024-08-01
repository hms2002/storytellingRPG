using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : KeywordSup
{
    private void Awake()
    {
        keywordName = "거대한";
        SetKeywordColor(Y);
        keywordTension = -8;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateDatabase.stateDatabase.stonePiece, 2);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
