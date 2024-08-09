using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipwreckedGoblin_Rowing : KeywordMain
{
    private void Awake()
    {
        keywordName = "노 젓기";

        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
