using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeWater : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(D);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.hp += keywordHeal;
    }

    public override void Check(KeywordSup keywordSup)
    {

    }
}
