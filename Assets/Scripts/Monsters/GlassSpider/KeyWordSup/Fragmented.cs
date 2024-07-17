using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragmented : KeywordSup
{
    GlassSpider glassSpider;


    private void Awake()
    {
        keywordName = "파편화된";
        SetKeywordColor(RED);
        keywordTension = 5;
    }

    public override void Execute(Actor caster, Actor target)
    {
        glassSpider = caster as GlassSpider;
        caster.damage += glassSpider.glassFragmentStack;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
