using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratch : KeywordMain
{
    private void Awake()
    {
        SetKeywordColor(RED);
        keywordDamage = 3;
        keyWordTension = -20;
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
