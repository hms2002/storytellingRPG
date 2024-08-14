using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossed : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += caster.protect;
    }

    public override void Check(KeywordMain _keywordMain) { }
}
