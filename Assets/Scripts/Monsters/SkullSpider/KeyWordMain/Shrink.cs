using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : KeywordMain
{
    private void Awake()
    {
        keywordName = "웅크리기";
        SetKeywordColor(BLUE);
        keywordProtect = 10;
        keyWordTension = -21;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;
        caster.tension += keyWordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
