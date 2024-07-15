using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biting : KeywordMain
{
    private void Awake()
    {
        keywordName = "¹°¾î¶â±â";
        SetKeywordColor(RED);
        keywordDamage = 10;
        keyWordTension = 10;
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
