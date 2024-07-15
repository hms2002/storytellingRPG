using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : KeywordSup
{
    [Header("붉은 키워드 추가 데미지")]
    [SerializeField] private int extraDamage = 3;


    private void Awake()
    {
        keywordName = "붉은";
        SetKeywordColor(RED);
        keywordDamage = 0;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage + extraDamage;
    }

    public override void Check(KeywordMain _keywordMain)
    {
        if (_keywordMain.GetKeywordColor() == RED)// Main 키워드의 분류 색이 빨간색이면
        {
            extraDamage = 3;
        }
        else // ���� Ű������ ���� �������� �ƴϸ�
        {
            extraDamage = 0;
        }
    }
}
