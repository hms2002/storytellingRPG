using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Anger : KeywordMain
{
    [Header("부여되는 일회성 강화 수치 제어")]
    [SerializeField] private int amountOfoneTimeReinforce = 5;


    private void Awake()
    {
        keywordName = "분노";
        SetKeywordColor(BLUE);
        keywordTension = -20;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.oneTimeReinforce += amountOfoneTimeReinforce;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}