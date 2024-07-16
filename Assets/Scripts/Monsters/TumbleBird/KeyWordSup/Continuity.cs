using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continuity : KeywordSup
{
    private void Awake()
    {
        SetKeywordColor(BLUE);
        keywordProtect = 5;
        keywordTension = -6;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect = keywordProtect;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
