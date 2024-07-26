using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWeb : KeywordMain
{
    private void Awake()
    {
        keywordName = "거미줄";
        SetKeywordColor(R);
        keywordDamage = 5;
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
