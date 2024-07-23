using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defensive : KeywordSup
{
    private void Awake()
    {
        keywordName = "방어적인";
        SetKeywordColor(B);
        keywordProtect = 4;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
