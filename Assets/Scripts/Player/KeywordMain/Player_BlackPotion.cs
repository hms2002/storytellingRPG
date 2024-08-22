using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BlackPotion : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "검은 포션";
        SetKeywordColor(D);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.hp += buffStack;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
