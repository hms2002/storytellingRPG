using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joyful : KeywordSup
{
    private void Awake()
    {
        keywordName = "즐거운";
        SetKeywordColor(R);
        keywordDamage = 5;
        keywordTension = 8;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
