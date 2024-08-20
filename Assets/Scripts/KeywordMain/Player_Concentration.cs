using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Concentration : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "집중";
        SetKeywordColor(B);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += 7;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
