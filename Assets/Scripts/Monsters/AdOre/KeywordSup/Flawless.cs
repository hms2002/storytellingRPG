using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flawless : KeywordSup
{
    [Header("부여되는 강화 수치 제어")]
    [SerializeField] private int amountOfEnhance = 3;


    private void Awake()
    {
        keywordName = "쪼개진";
        SetKeywordColor(R);
        keywordTension = 8;
    }

    public override void Execute(Actor caster, Actor target)
    {
        //caster.damage += amountOfDamage;

        target.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
