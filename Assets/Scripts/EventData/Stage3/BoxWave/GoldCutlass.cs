using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCutlass : KeywordSup
{
    [Header("주는 골드량")]
    [SerializeField]
    int goldAmount;
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(Y);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        caster.gold += 10;
    }

    public override void Check(KeywordMain keywordMain)
    {

    }
}
