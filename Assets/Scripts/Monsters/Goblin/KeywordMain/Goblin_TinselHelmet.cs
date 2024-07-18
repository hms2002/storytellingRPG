using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_TinselHelmet : KeywordMain
{
    [Header("부여되는 방어 수치 제어")]
    [SerializeField] private int amountOfProtect = 8;


    private void Awake()
    {
        keywordName = "양철 투구";
        SetKeywordColor(BLUE);
        keywordTension = -12;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += amountOfProtect;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
