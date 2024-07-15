using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scales : KeywordMain
{

    private void Awake()
    {
        keywordName = "ºñ´Ã";
        SetKeywordColor(BLUE);
        keywordProtect = 30;
        keyWordTension = -24;
    }
    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;
        caster.tension += keyWordTension;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
