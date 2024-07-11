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
    }
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        trasureDragon = caster as TrasureDragon;
        sentence.damage += keywordDamage;
        trasureDragon.dragonTrasure -= keywordDamage;
        trasureDragon.trasureDamage += keywordDamage;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
