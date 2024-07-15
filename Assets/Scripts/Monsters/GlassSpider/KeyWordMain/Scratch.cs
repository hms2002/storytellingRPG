using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratch : KeywordMain
{
    private void Awake()
    {
        keywordName = "ÇÒÄû±â";
        SetKeywordColor(RED);
        keywordDamage = 3;
        keyWordTension = -20;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        caster.tension += keyWordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
