using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restored : KeywordSup
{
    GlassSpider glassSpider;
    private void Awake()
    {
        SetKeywordColor(BLUE);
        keyWordTension = -20;
    }

    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        glassSpider = caster as GlassSpider;
        glassSpider.glassFragmentStack -= 1;
        sentence.tension += keyWordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
