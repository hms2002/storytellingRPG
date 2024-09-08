using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Betrayal : KeywordMain
{
    // Start is called before the first frame update
    void Awake()
    {
        keywordName = "배신감";
        SetKeywordColor(Y);
        keywordTension = 12;
        effectTarget = EffectTarget.target;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        if(target.protect != 0)
        {
            target.protect = 0;
        }
        else
        {
            target.charactorState.AddState(StateDatabase.stateDatabase.oneTimeReduction, debuffStack);
        }

        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
        
    }
}
