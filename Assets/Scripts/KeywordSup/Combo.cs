using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : KeywordSup
{
    [Header("연속된 키워드 추가 공격 횟수")]
    [SerializeField] private int repeatCount = 1;


    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "연속된";
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.repeatStack += buffStack;
    }

    public override void Check(KeywordMain _keywordMain) { }
}
