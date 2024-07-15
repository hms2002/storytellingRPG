using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shiny : KeywordSup
{
    GlassSpider glassSpider;


    private void Awake()
    {
        keywordName = "¹ÝÂ¦ÀÌ´Â";
        SetKeywordColor(RED);
        keywordDamage = 3;
        keyWordTension = 8;
    }

    public override void Execute(Actor caster, Actor target)
    {
        glassSpider = caster as GlassSpider;
        caster.damage = keywordDamage;
        caster.tension += keyWordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
