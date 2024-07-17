using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedHerb : KeywordSup
{
    PotionGlub potionGlub;

    [Header("���� ���Ǽ�ġ ����")]
    [SerializeField] private int maxRange = 2;
    [SerializeField] private int minRange = 1;

    private void Awake()
    {
        keywordName = "���� ����";
        SetKeywordColor(BLUE);
        keywordTension = -3;
    }

    public override void Execute(Actor caster, Actor target)
    {
        potionGlub = caster as PotionGlub;
        potionGlub.potionNum = Random.Range(minRange, maxRange);
        potionGlub.ColorChecking();
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
