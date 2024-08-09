using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : KeywordSup
{


    private void Awake()
    {
        keywordName = "연속된";
        isPlayerKeyword = true;
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.repeatStack += buffStack;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
