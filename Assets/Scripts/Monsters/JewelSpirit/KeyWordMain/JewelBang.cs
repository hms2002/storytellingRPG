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
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.dmgList.Add(keywordDamage);
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
