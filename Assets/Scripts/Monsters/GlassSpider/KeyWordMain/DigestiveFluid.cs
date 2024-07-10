using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigestiveFluid : KeywordMain
{
    private void Awake()
    {
        SetKeywordColor(RED);
        keywordDamage = 2;
        debuffType = "Reduction";
        debuffStack = 3;
    }
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.damage += keywordDamage;
        target.reductionStack += debuffStack;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
