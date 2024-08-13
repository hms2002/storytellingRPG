using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSword : KeywordMain
{
    private void Awake()
    {
        SetKeywordColor(R);
        keywordDamage = 7;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += 7;
    }

    public override void Check(KeywordSup _keywordSup) { }
}
