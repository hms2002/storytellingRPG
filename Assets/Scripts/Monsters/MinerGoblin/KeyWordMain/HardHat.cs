using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardHat : KeywordMain
{
    // Start is called before the first frame update
    void Awake()
    {
        keywordName = "안전모";
        SetKeywordColor(B);
        keywordTension = -15;
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
