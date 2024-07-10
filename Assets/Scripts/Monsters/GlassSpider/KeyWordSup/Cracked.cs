using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cracked : KeywordSup
{
    GlassSpider glassSpider;
    private void Awake()
    {
        SetKeywordColor(BLUE);
        keywordProtect = 5;
    }

    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        glassSpider = caster as GlassSpider;
        glassSpider.protect += keywordProtect;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
