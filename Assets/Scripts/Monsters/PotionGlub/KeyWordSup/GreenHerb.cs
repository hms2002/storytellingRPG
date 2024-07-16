using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenHerb : KeywordSup
{
    PotionGlub potionGlub;
    [Header("���� ���Ǽ�ġ ����")]
    [SerializeField] private int maxRange = 3;
    [SerializeField] private int minRange = 2;
    private void Awake()
    {
        keywordName = "�ʷ� ����";
        SetKeywordColor(BLUE);
        keywordTension = -7;
    }

    public override void Execute(Actor caster, Actor target)
    {
        potionGlub = caster as PotionGlub;
        potionGlub.potionNum += Random.Range(minRange,maxRange);
        potionGlub.ColorChecking();
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
