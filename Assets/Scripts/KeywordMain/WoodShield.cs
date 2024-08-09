using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodShield : KeywordMain
{
    private void Awake()
    {
        keywordName = "나무방패";
        isPlayerKeyword = true;
        SetKeywordColor(B);
        keywordProtect = 6;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
