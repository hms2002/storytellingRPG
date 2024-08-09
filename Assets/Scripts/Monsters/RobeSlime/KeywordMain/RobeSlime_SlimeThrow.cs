using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobeSlime_SlimeThrow : KeywordMain
{
    private void Awake()
    {
        keywordName = "점액질 투척";

        SetKeywordColor(R);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        caster.tension += keywordTension;
    }
}
