using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointed : KeywordSup
{
    private void Awake()
    {
        SetKeywordColor(RED);
        keywordDamage = 5;
        keywordTension = 8;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.nextTurnDamage += keywordDamage;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
