using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoorestMaster_Lush : KeywordSup
{
    private void Awake()
    {
        keywordName = "울창한";

        SetKeywordColor(B);
        keywordTension = -10;
        debuffStack = 4;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += debuffStack;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
