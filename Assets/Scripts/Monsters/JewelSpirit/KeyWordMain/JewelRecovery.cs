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
        if(caster.beforeDamage > 0)
        {
            keywordProtect = caster.beforeDamage;
            if(caster.beforeDamage > 7)
            {
                keywordProtect = 7;
            }
        }
        caster.protect += keywordProtect;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
