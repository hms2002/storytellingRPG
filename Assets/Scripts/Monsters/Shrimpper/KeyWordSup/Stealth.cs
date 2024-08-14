using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth : KeywordSup
{
    private void Awake()
    {
        keywordName = "은엄폐";
        SetKeywordColor(B);
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
