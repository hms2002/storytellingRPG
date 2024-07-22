using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restored : KeywordSup
{
    GlassSpider glassSpider;

    [Header("수복된 키워드 유리 파편 제거량")]
    [SerializeField] private int removeAmount = 1;


    private void Awake()
    {
        keywordName = "수복된";
        SetKeywordColor(BLUE);
        keywordTension = -20;
    }

    public override void Execute(Actor caster, Actor target)
    {
        glassSpider = caster as GlassSpider;
        glassSpider.charactorState.ReductionByValue(StateType.glassPragment, removeAmount);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
