using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealedGargoyle_WingShield : KeywordMain
{
    private void Awake()
    {
        keywordName = "날개 방패";

        SetKeywordColor(B);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;
        caster.tension += keywordTension;
    }
}
