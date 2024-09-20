using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelRecovery : KeywordMain
{
    private void Awake()
    {
        keywordName = "보석 복구";
        SetKeywordColor(B);
        keywordProtect = 4;
        keywordTension = 18;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.tension += keywordTension;
        if(caster.lastTurnProtectReduction > 0)
        {
            keywordProtect = caster.lastTurnProtectReduction;
            if(caster.lastTurnProtectReduction > 7)
            {
                keywordProtect = 7;
            }
        }
        else
        {
            caster.protect += keywordProtect;
        }
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
