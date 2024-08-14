using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharp : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
    }

    public override void Check(KeywordMain _keywordMain) { }
}
