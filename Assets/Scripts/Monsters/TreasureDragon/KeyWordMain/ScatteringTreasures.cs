using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatteringTreasures : KeywordMain
{
    TrasureDragon trasureDragon;
    private void Awake()
    {
        SetKeywordColor(RED);
        keywordDamage = Random.Range(12, 18);
        keyWordTension = -14;
    }
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        trasureDragon = caster as TrasureDragon;
        sentence.damage += keywordDamage;
        trasureDragon.dragonsTrasure -= keywordDamage;
        trasureDragon.trasureDamage += keywordDamage;
        sentence.tension += keyWordTension;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
