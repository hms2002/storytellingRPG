using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragmented : KeywordSup
{
    GlassSpider glassSpider;


    private void Awake()
    {
        keywordName = "파편화된";
        SetKeywordColor(R);
        keywordTension = 5;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        glassSpider = caster as GlassSpider;
        caster.dmgList.Plus(glassSpider.charactorState.GetStateStack(StateType.glassPragment));
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
