using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flaccid : KeywordSup
{
    private void Awake()
    {
        keywordName = "흐물거리는";
        SetKeywordColor(B);
        keywordProtect = 5;
        keywordTension = -5;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
