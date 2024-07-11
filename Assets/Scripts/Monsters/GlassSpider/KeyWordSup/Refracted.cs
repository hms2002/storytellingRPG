using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refracted : KeywordSup
{
    GlassSpider glassSpider;
    private void Awake()
    {
        SetKeywordColor(BLUE);
        keywordProtect = 5;
        keyWordTension = -10;
    }

    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        glassSpider = caster as GlassSpider;
        glassSpider.protect += keywordProtect;
        sentence.tension += keyWordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
