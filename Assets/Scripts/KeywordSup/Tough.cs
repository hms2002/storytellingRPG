using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tough : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "강인한";
        SetKeywordColor(B);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;
    }

    public override void Check(KeywordMain _keywordMain) { }
}
