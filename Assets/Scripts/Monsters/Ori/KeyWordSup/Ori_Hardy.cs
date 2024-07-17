using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ori_Hardy : KeywordSup
{
    private void Awake()
    {
        keywordName = "두려운";

        SetKeywordColor(BLUE);
        keywordTension = 3;
        debuffStack = 1;
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
