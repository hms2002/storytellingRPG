using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigestiveFluid : KeywordMain
{
    private void Awake()
    {
        keywordName = "소화액";
        SetKeywordColor(R);
        keywordDamage = 2;
        debuffType = "Reduction";
        debuffStack = 3;
        keywordTension = 10;
        Init();
    }
    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        target.charactorState.AddState(StateDatabase.stateDatabase.
            reduction, debuffStack);
        caster.tension += keywordTension;
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }
}
