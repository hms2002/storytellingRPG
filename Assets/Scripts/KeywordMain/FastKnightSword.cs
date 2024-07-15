using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastKnightSword : KeywordMain
{
    [Header("�ӱ���� �� ���� Ƚ��")]
    [SerializeField] private int attackNum = 2;

    private void Awake()
    {
        keywordName = "�ӱ���� ��";
        SetKeywordColor(RED);
        keywordDamage = 2;
    }
    
    public override void Execute(Actor caster, Actor target)
    {
        caster.repeatStack *= (attackNum);
        caster.damage += (keywordDamage);
    }

    public override void Check(KeywordSup keywordSup)
    {
    }
}
