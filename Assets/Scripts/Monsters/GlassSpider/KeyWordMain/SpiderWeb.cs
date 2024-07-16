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
        keywordTension = -10;
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
