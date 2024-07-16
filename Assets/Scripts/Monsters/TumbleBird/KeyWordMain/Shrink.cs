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
        keywordTension = -9;
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
