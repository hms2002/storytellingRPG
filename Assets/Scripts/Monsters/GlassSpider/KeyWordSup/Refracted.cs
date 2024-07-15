using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refracted : KeywordSup
{
    GlassSpider glassSpider;


    private void Awake()
    {
        keywordName = "±¼ÀýµÈ";
        SetKeywordColor(BLUE);
        keywordProtect = 5;
        keyWordTension = -10;
    }

    public override void Execute(Actor caster, Actor target)
    {
        glassSpider = caster as GlassSpider;
        glassSpider.protect += keywordProtect;
        caster.tension += keyWordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
