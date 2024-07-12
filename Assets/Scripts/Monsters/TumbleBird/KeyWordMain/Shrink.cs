using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : KeywordMain
{
    private void Awake()
    {
        SetKeywordColor(RED);
        keywordDamage = Random.Range(6, 10);
        keyWordTension = -9;
    }
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.damage += keywordDamage;
        sentence.tension += keyWordTension;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
