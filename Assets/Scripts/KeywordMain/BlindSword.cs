using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindSword : KeywordMain
{
    [Header("맹목의 검 추가 부여 스택 수치")]
    [SerializeField] private int stack = 1;


    private void Awake()
    {
        keywordName = "맹목의 검";
        SetKeywordColor(R);
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
