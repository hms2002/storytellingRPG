using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restored : KeywordSup
{
    GlassSpider glassSpider;

    [Header("������ Ű���� ���� ���� ���ŷ�")]
    [SerializeField] private int removeAmount = 1;


    private void Awake()
    {
        keywordName = "������";
        SetKeywordColor(BLUE);
        keywordTension = -20;
    }

    public override void Execute(Actor caster, Actor target)
    {
        glassSpider = caster as GlassSpider;
        glassSpider.glassFragmentStack -= removeAmount;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
