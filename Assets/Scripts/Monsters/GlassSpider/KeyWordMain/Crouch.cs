using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : KeywordMain
{
    private void Awake()
    {
        keywordName = "웅크리기";
        SetKeywordColor(B);
        keywordProtect = 10;
        keywordTension = 6;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect = 10;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
