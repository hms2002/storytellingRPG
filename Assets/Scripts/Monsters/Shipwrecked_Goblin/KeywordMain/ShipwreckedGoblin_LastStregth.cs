using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipwreckedGoblin_LastStregth : KeywordMain
{
    private void Awake()
    {
        keywordName = "마지막 힘";

        SetKeywordColor(R);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        int debuffStack = caster.charactorState.AllDebuffStack();
        if (debuffStack > 30) debuffStack = 30;
        caster.damage += debuffStack;
        caster.tension += keywordTension;
    }
}
