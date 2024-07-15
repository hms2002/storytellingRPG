using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweep : KeywordMain
{
    TrasureDragon trasureDragon;
    [Header("������ �������� ������,���� ������ ����")]
    [SerializeField] private int trasureDamage = 10;
    [SerializeField] private int maxRange = 18;
    [SerializeField] private int minRange = 15;

    private void Awake()
    {
        keywordName = "�۾���";
        SetKeywordColor(RED);
        keywordDamage = Random.Range(minRange, maxRange);
        keyWordTension = 18;
    }
    public override void Execute(Actor caster, Actor target)
    {
        trasureDragon = caster as TrasureDragon;
        trasureDragon.damage += keywordDamage;
        trasureDragon.dragonsTrasure -= 10;
        trasureDragon.trasureDamage += trasureDamage;
        caster.tension += keyWordTension;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
