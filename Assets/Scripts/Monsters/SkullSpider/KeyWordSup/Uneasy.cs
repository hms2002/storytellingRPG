using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uneasy : KeywordSup
{ 
    private void Awake()
    {
        keywordName = "불안한";

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

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
