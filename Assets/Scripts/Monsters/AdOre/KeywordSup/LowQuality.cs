using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowQuality : KeywordSup
{
    [Header("부여되는 취약 수치 제어")]
    [SerializeField] private int amountOfWeakenStack = 2;


    private void Awake()
    {
        keywordName = "저품질";
        SetKeywordColor(RED);
        keywordTension = -28;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.weakenStack += amountOfWeakenStack;

        target.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
