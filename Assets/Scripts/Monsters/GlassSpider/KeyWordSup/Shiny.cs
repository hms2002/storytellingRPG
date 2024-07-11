using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shiny : KeywordSup
{
    GlassSpider glassSpider;
    private void Awake()
    {
        SetKeywordColor(RED);
        keywordDamage = 3;
        keyWordTension = 8;
    }

    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        glassSpider = caster as GlassSpider;
        sentence.damage = keywordDamage;
        sentence.tension += keyWordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
