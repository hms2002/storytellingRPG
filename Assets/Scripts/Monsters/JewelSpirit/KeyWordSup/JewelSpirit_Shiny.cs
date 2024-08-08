using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelSpirit_Shiny : KeywordSup
{

    private void Awake()
    {
        keywordName = "반짝이는";
        SetKeywordColor(R);
        keywordTension = -10;
        keywordDamage = 5;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage = keywordDamage;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
