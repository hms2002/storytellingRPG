using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burning : KeywordSup
{
    private void Awake()
    {
        keywordName = "ºÒÅ¸´Â";
        SetKeywordColor(RED);
        debuffType = "Burn";
        debuffStack = 2;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.burnStack += (debuffStack);
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
