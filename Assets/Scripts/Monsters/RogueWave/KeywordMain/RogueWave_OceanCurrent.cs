using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueWave_OceanCurrent : KeywordMain
{
    private void Awake()
    {
        keywordName = "해류";

        SetKeywordColor(Y);
        keywordTension = -40;
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.tension += keywordTension;
    }
}
