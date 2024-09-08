using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : KeywordMain
{
    private void Awake()
    {
        keywordName = "웅크리기";
        keywordTension = 6;
        SetKeywordColor(B);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect = keywordProtect;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
