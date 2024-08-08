using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JewelBang : KeywordMain
{
    private void Awake()
    {
        keywordName = "보석 강타";
        SetKeywordColor(R);
        keywordDamage = 6;
        keywordTension = -10;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
        if(_keywordSup.keywordProtect > 0)
        {
            keywordDamage += _keywordSup.keywordProtect;
        }
    }
}
