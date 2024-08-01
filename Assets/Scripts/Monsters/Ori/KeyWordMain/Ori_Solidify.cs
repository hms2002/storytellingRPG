using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ori_Solidify : KeywordMain
{
    private void Awake()
    {
        keywordName = "굳히기";

        SetKeywordColor(B);
        keywordProtect = 8;
        keywordTension = -12;
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
