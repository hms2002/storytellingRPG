using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fantastic : KeywordSup
{
    TrasureDragon trasureDragon;

    [Header("���� ���� ������ ��ġ")]
    [SerializeField] private int damageFigures = 20;


    private void Awake()
    {
        keywordName = "ȯ������";
        SetKeywordColor(BLUE);
        keyWordTension = -11;
    }

    public override void Execute(Actor caster, Actor target)
    {
        trasureDragon = caster as TrasureDragon;
        trasureDragon.dragonsTrasure -= damageFigures;
        trasureDragon.trasureDamage += damageFigures;
        caster.tension += keyWordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
