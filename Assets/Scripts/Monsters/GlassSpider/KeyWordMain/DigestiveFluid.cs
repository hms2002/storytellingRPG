using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigestiveFluid : KeywordMain
{
    private void Awake()
    {
        keywordName = "¼ÒÈ­¾×";
        SetKeywordColor(RED);
        keywordDamage = 2;
        debuffType = "Reduction";
        debuffStack = 3;
        keyWordTension = 10;
    }
    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        target.reductionStack += debuffStack;
        caster.tension += keyWordTension;
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }
}
