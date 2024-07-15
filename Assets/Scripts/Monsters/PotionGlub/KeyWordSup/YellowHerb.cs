using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowHerb : KeywordSup
{
    PotionGlub potionGlub;

    [Header("���� ���Ǽ�ġ ����")]
    [SerializeField] private int maxRange = 4;
    [SerializeField] private int minRange = 2;

    private void Awake()
    {
        keywordName = "��� ����";
        SetKeywordColor(BLUE);
        keyWordTension = -10;
    }

    public override void Execute(Actor caster, Actor target)
    {
        potionGlub = caster as PotionGlub;
        potionGlub.potionNum = Random.Range(minRange, maxRange);
        potionGlub.ColorChecking();
        caster.tension += keyWordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}