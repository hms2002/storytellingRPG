using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking : KeywordMain
{
    PotionGlub potionGlub;

    [Header("���� ���Ǽ�ġ ����")]
    [SerializeField] private int maxRange = 3;
    [SerializeField] private int minRange = -3;

    private void Awake()
    {
        keywordName = "����ŷ";
        SetKeywordColor(BLUE);
        keywordTension = -5;
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
