using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationFear : KeywordMain
{
    private void Awake()
    {
        keywordName = "공포감조성";

        SetKeywordColor(RED);
        keywordTension = -10;
        keywordDamage = 3;
        debuffStack = 1;
        debuffType = "Fear";    
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        target.charactorState.AddState(StateDatabase.stateDatabase.fear, debuffStack);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}
