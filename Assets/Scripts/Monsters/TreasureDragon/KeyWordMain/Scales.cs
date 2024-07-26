using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scales : KeywordMain
{

    private void Awake()
    {
        keywordName = "비늘";
        SetKeywordColor(B);
        keywordProtect = 30;
        keywordTension = -24;
        Init();
    }
    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;
        caster.tension += keywordTension;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
