using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueWave_BoatDebris : KeywordMain
{
    private void Awake()
    {
        keywordName = "배 파편";

        SetKeywordColor(B);
        Init();
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;
        caster.tension += keywordTension;
    }
}
