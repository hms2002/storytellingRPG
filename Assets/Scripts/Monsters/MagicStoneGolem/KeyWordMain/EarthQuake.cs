using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuake : KeywordMain
{
    private void Awake()
    {
        keywordName = "대지 울리기";

        SetKeywordColor(R);
        keywordTension = -16;
        keywordDamage = 7;
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
