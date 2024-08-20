using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Roar : KeywordMain
{
    private void Awake()
    {
        keywordName = "포효";
        isPlayerKeyword = true;
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += caster.protect;
        caster.protect = 0;

    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
