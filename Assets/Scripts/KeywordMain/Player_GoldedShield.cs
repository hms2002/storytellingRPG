using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GoldedShield : KeywordMain
{
    [SerializeField] int gainGold = 7;
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "황금 방패";
        SetKeywordColor(B);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;
        caster.gold += gainGold;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
