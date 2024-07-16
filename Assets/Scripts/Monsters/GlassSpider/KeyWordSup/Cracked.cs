using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cracked : KeywordSup
{
    GlassSpider glassSpider;

    [Header("������ Ű���� ���� ���� ����")]
    [SerializeField] private int stack = 1;


    private void Awake()
    {
        keywordName = "������";
        SetKeywordColor(BLUE);
        keywordTension = 20; 
    }

    public override void Execute(Actor caster, Actor target)
    {
        glassSpider = caster as GlassSpider;
        glassSpider.glassFragmentStack += stack;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
