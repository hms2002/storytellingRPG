using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twingkle : KeywordSup
{
    private void Awake()
    {
        keywordName = "반짝이는";
        SetKeywordColor(B);
        keywordProtect = 5;
        keywordTension = -6;
        Init();
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
