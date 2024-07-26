using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biting : KeywordMain
{
    private void Awake()
    {
        keywordName = "물어뜯기";
        SetKeywordColor(R);
        keywordDamage = 10;
        keywordTension = 10;
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
