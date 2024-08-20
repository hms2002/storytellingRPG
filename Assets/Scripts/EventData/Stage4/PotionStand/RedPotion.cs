using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPotion : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(D);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
    }

    public override void Check(KeywordSup keywordSup)
    {

    }
}
