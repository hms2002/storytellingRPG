using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translucent : KeywordSup
{

    private void Awake()
    {
        keywordName = "반투명한";

        SetKeywordColor(B);
        keywordTension = 5;
        keywordProtect = 3;
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
