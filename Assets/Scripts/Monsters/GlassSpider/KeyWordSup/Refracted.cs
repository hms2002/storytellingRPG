using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refracted : KeywordSup
{
    GlassSpider glassSpider;
    private void Awake()
    {
        SetKeywordColor(BLUE);
    }

    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        glassSpider = caster as GlassSpider;
        glassSpider.glassFragmentStack += 1;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
