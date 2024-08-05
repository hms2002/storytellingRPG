using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootTentacles_Whipping : KeywordMain
{
    private void Awake()
    {
        keywordName = "채찍질";
        SetKeywordColor(R);
        keywordDamage = 2;
        keywordTension = -8;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
