using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratch : KeywordMain
{
    private void Awake()
    {
        keywordName = "ÇÒÄû±â";
        SetKeywordColor(R);
        keywordDamage = 3;
        keywordTension = -20;
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
