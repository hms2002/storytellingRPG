using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stirring : KeywordMain
{
    PotionGlub potionGlub;

    [Header("���� ���Ǽ�ġ ����")]
    [SerializeField] private int maxRange = 3;
    [SerializeField] private int minRange = 1;

    private void Awake()
    {
        keywordName = "���͸�";
        SetKeywordColor(BLUE);
        keyWordTension = -8;
    }

    public override void Execute(Actor caster, Actor target)
    {
        potionGlub = caster as PotionGlub;
        potionGlub.potionNum = Random.Range(minRange, maxRange);
        potionGlub.ColorChecking();
        caster.tension += keyWordTension;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}

