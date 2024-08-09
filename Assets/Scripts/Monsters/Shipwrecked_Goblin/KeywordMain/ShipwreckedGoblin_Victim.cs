using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipwreckedGoblin_Victim : KeywordMain
{
    private void Awake()
    {
        keywordName = "조난자";

        SetKeywordColor(R);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += target.charactorState.AllDebuffStack();
        caster.tension += keywordTension;
    }
}
