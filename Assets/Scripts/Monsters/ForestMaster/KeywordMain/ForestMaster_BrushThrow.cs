using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestMaster_BrushThrow : KeywordMain
{
    private void Awake()
    {
        keywordName = "수풀 던지기";
        SetKeywordColor(R);
        keywordDamage = 3;
        keywordTension = -10;
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
