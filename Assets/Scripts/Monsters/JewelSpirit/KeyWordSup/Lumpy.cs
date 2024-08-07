using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumpy : KeywordSup
{
    private void Awake()
    {
        keywordName = "덩어리 진";

        SetKeywordColor(B);
        keywordTension = 7;
        keywordProtect = 4;
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
