using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DoubleEagleSwordOfSevenKingdom : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "공방 협차";
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        for (int i = 0; i < 7; i++)
            caster.dmgList.Add(keywordDamage);
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
