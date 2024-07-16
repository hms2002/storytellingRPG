using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : KeywordMain
{
    PotionGlub potionGlub;

    [Header("���� ���Ǽ�ġ ����")]
    [SerializeField] private int maxRange = 1;
    [SerializeField] private int minRange = -2;

    private void Awake()
    {
        keywordName = "����";
        SetKeywordColor(BLUE);
        keywordTension = -4;
    }

    public override void Execute(Actor caster, Actor target)
    {
        potionGlub = caster as PotionGlub;
        potionGlub.potionNum = Random.Range(minRange, maxRange);
        potionGlub.ColorChecking();
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}
