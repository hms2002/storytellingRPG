using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biting : KeywordMain
{
    private void Awake()
    {
        SetKeywordColor(RED);
        keywordDamage = 10;
        keyWordTension = 10;
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
