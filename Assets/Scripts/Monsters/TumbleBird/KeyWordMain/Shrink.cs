using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : KeywordMain
{
    private void Awake()
    {
        keywordName = "¿õÅ©¸®±â";
        SetKeywordColor(RED);
        keywordDamage = Random.Range(6, 10);
        keyWordTension = -9;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        caster.tension += keyWordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
