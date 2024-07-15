using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointed : KeywordSup
{
    private void Awake()
    {
        SetKeywordColor(RED);
        keywordDamage = 5;
        keyWordTension = 8;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.nextTurnDamage += keywordDamage;
        caster.tension += keyWordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
