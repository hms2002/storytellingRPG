using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Defensive : KeywordSup
{
    private void Awake()
    {
        keywordName = "방어적인";
        SetKeywordColor(B);
        keywordProtect = 4;
        keywordTension = -4;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;

        target.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
