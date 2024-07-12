using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweep : KeywordMain
{
    TrasureDragon trasureDragon;

    private void Awake()
    {
        SetKeywordColor(RED);
        keywordDamage = Random.Range(15, 18);
        keyWordTension = 18;
    }
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        trasureDragon = caster as TrasureDragon;
        sentence.damage += keywordDamage;
        trasureDragon.dragonTrasure -= 10;
        trasureDragon.trasureDamage += 10;
        sentence.tension += keyWordTension;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
