using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : KeywordMain
{
    private void Awake()
    {
        keywordName = "πÊ∆–";
        SetKeywordColor(B);
        keywordProtect = 5;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
