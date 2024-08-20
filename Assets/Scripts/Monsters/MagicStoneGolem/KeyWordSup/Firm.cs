using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firm : KeywordSup
{
    private void Awake()
    {
        keywordName = "굳어진";

        SetKeywordColor(B);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;
        caster.tension += keywordTension;
    }
}
