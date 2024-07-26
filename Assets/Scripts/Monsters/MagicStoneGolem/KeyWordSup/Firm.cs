using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firm : KeywordSup
{
    private void Awake()
    {
        keywordName = "굳어진";
        SetKeywordColor(B);
        keywordProtect = 10;
        keywordTension = -16;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
