using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySlam : KeywordMain
{
    private void Awake()
    {
        keywordName = "몸통 박치기";

        SetKeywordColor(RED);
        keywordTension = -10;
        keywordDamage = 3;
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
