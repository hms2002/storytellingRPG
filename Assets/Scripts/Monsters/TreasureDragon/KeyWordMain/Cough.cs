using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cough : KeywordMain
{
    private void Awake()
    {
        SetKeywordColor(RED);
        keywordDamage = Random.Range(20,30);
        debuffStack = 5;
        debuffType = "Burn";
    }
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.damage += keywordDamage;
        sentence.burnStack += debuffStack;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
