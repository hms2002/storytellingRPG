using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ChargeShield : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "돌격 방패";
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.dmgList.Add(caster.protect);
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
