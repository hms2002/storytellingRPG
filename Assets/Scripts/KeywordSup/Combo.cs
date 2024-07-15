using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : KeywordSup
{
    [Header("���ӵ� ���� Ƚ��")]
    private int repeatNum = 1;


    private void Awake()
    {
        keywordName = "���ӵ�";
        SetKeywordColor(RED);
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.repeatStack += repeatNum;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
