using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twingkle : KeywordSup
{
    private void Awake()
    {
        keywordName = "¹ÝÂ¦ÀÌ´Â";
        SetKeywordColor(B);
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
