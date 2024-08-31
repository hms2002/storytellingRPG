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
        if(caster.protect >= 7)
            caster.dmgList.Add(caster.protect);
        

    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
