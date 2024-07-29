using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : KeywordMain
{
    private void Awake()
    {
        keywordName = "포션";
        SetKeywordColor(D);
        keywordHeal = 12;
        Init();
        isOneTimeUse = true;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.heal += keywordHeal;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
