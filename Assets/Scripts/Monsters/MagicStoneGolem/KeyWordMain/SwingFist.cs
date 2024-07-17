using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingFist : KeywordMain
{ 
    private void Awake()
    {
        keywordName = "주먹 휘두르기";

        SetKeywordColor(BLUE);
        keywordTension = 6;
        debuffStack = 2;
        debuffType = "Fear";
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.fearStack += debuffStack;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}
