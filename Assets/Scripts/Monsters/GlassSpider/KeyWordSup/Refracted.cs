using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refracted : KeywordSup
{
    GlassSpider glassSpider;


    private void Awake()
    {
        keywordName = "굴절된";
        SetKeywordColor(B);
        keywordProtect = 5;
        keywordTension = -10;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        glassSpider = caster as GlassSpider;
        glassSpider.protect += keywordProtect;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
