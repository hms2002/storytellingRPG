using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ori_Hardy : KeywordSup
{
    private void Awake()
    {
        keywordName = "튼튼한";

        SetKeywordColor(B);
        keywordProtect = 5;
        keywordTension = -4;
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
