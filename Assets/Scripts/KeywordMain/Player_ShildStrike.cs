using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ShildStrike : KeywordMain
{
    private void Awake()
    {
        keywordName = "방패 강타";
        isPlayerKeyword = true;
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.dmgList.Add(caster.protect);
        caster.protect = 0;

    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
