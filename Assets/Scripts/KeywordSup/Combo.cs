using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : KeywordSup
{
    [Header("연속된 공격 횟수")]
    private int repeatNum = 1;


    private void Awake()
    {
        keywordName = "연속된";
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.repeatStack += repeatNum;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
