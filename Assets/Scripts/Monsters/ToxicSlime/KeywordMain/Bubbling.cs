using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubbling : KeywordMain
{
    private void Awake()
    {
        keywordName = "보글거리기";
        SetKeywordColor(B);
        keywordDamage = 2;
        keywordTension = 5;
        debuffStack = 1;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage = keywordDamage;
        
        target.charactorState.AddState
            (StateDatabase.stateDatabase.addiction, debuffStack);
        
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}