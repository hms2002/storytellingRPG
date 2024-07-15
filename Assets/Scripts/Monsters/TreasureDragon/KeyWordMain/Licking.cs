using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liking : KeywordMain
{
    [Header("���� ������ ����")]
    [SerializeField] private int maxRange = 10;
    [SerializeField] private int minRange = 6;

    private void Awake()
    {
        keywordName = "�ӱ�";
        SetKeywordColor(RED);
        keywordDamage = Random.Range(minRange, maxRange);
        keyWordTension = -9;
    }
    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        caster.tension += keyWordTension;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
