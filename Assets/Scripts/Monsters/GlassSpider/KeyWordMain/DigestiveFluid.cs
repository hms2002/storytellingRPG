using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigestiveFluid : KeywordMain
{
    private void Awake()
    {
        keywordName = "소화액";
        SetKeywordColor(R);
        keywordTension = 10;
        Init();
    }
    public override void Execute(Actor caster, Actor target)
    {
        caster.dmgList.Add(keywordDamage);
        target.charactorState.AddState(StateDatabase.stateDatabase.
            reduction, debuffStack);
        caster.tension += keywordTension;
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }
}
