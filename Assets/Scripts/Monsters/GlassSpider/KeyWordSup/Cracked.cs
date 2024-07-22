using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cracked : KeywordSup
{
    GlassSpider glassSpider;

    [Header("깨어진 키워드 유리 파편 스택")]
    [SerializeField] private int stack = 1;


    private void Awake()
    {
        keywordName = "깨어진";
        SetKeywordColor(BLUE);
        keywordTension = 20; 
    }

    public override void Execute(Actor caster, Actor target)
    {
        glassSpider = caster as GlassSpider;
        glassSpider.charactorState.AddState
            (StateDatabase.stateDatabase.glassPragment, stack);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
