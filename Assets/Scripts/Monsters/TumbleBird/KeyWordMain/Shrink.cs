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
        keywordTension = -21;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
