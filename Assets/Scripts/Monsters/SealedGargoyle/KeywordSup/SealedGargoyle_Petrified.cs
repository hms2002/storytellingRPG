using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealedGargoyle_Petrified : KeywordSup
{
    private void Awake()
    {
        keywordName = "석화된";

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
