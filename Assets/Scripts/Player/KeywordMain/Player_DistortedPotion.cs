using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DistortedPotion : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "왜곡된 포션";
        SetKeywordColor(D);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.protect = 0;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
