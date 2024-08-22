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
        caster.dmgList.Add(keywordDamage);
        if(caster.protect >= 6)
            caster.protect += keywordProtect;

    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
