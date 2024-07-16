using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHerb : KeywordSup
{
    PotionGlub potionGlub;

    [Header("���� ���Ǽ�ġ ����")]
    [SerializeField] private int maxRange = -1;
    [SerializeField] private int minRange = -5;

    private void Awake()
    {
        keywordName = "���� ����";
        SetKeywordColor(BLUE);
        keywordTension = 10;
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
