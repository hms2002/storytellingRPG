using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afreid : KeywordSup
{
    private void Awake()
    {
        keywordName = "두려운";

        SetKeywordColor(B);
        keywordTension = 3;
        debuffStack = 1;
        debuffType = "Fear";
        effectTarget = EffectTarget.target;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateDatabase.stateDatabase.fear, debuffStack);
        caster.tension += keywordTension;
        caster.attackSound = "타격음_주먹2";
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
