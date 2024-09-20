using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_TinselHelmet : KeywordMain
{
    private void Awake()
    {
        keywordName = "양철 투구";
        SetKeywordColor(B);
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
