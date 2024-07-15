using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWeb : KeywordMain
{
    private void Awake()
    {
        keywordName = "∞≈πÃ¡Ÿ";
        SetKeywordColor(RED);
        keywordDamage = 5;
        keyWordTension = -10;
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
