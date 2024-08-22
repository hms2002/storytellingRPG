using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Courage : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "용기";
        SetKeywordColor(B);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        if (caster.lastTurnProtectReduction >= 7)
        {
            caster.protect += keywordProtect;
        }
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
