using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindSword : KeywordMain
{
    [Header("�͸��� �� �߰� �ο� ���� ��ġ")]
    [SerializeField] private int stack = 1;


    private void Awake()
    {
        keywordName = "�͸��� ��";
        SetKeywordColor(RED);
    }


    public override void Execute(Actor caster, Actor target)
    {

    }

    public override void Check(KeywordSup keywordSup)
    {
        if (keywordSup.debuffType == "Burn")
        {
            keywordSup.debuffStack += stack;
        }
        if (keywordSup.debuffType == "Weaken")
        {
            keywordSup.debuffStack += stack;
        }
    }
}
