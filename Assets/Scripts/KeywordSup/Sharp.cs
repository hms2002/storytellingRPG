using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharp : KeywordSup
{
    private void Awake()
    {
        keywordName = "날카로운";
        SetKeywordColor(RED);
        keywordDamage = 2;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
