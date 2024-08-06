using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitting : KeywordMain
{
    private void Awake()
    {
        keywordName = "침 뱉기";
        SetKeywordColor(R);
        keywordDamage = 4;
        keywordTension = 10;
        debuffStack = 2;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;

        target.charactorState.AddState(StateDatabase.stateDatabase.addiction, debuffStack);

        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
