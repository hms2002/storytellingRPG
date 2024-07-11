using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragmented : KeywordSup
{
    GlassSpider glassSpider;
    private void Awake()
    {
        SetKeywordColor(RED);
        keyWordTension = 5;
    }

    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        glassSpider = caster as GlassSpider;
        sentence.damage += glassSpider.glassFragmentStack;
        sentence.tension += keyWordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
