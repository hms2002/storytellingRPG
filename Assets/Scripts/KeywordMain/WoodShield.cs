using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodShield : KeywordMain
{
    private void Awake()
    {
        SetKeywordColor(B);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;
    }

    public override void Check(KeywordSup _keywordSup) { }
}
