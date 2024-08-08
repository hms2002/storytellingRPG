using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueWave_Cannon : KeywordMain
{
    private void Awake()
    {
        keywordName = "대포";

        SetKeywordColor(R);
        Init();
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        caster.tension += keywordTension;
    }
}
