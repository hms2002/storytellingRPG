using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SwordOfKnight : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "기사의 검";
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        caster.protect += keywordProtect;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
