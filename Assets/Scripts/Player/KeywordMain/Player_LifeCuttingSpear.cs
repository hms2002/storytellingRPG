using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_LifeCuttingSpear : KeywordMain
{
    private void Awake()
    {
        keywordName = "절명의 단창";
        isPlayerKeyword = true;
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.dmgList.Add(keywordDamage, true);
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
