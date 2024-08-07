using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreRay : KeywordMain
{
    private void Awake()
    {
        keywordName = "코어 광선";
        SetKeywordColor(R);
        keywordProtect = 25;
        keywordDamage = 35;
        keywordTension = -10;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect -= keywordProtect;
        caster.damage += keywordDamage;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
