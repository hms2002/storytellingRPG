using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : KeywordSup
{
    [Header("쪼개진 키워드 데미지 수치 제어")]
    [SerializeField] private int amountOfDamage = 5;


    private void Awake()
    {
        keywordName = "쪼개진";
        SetKeywordColor(RED);
        keywordTension = -8;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += amountOfDamage;

        target.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
