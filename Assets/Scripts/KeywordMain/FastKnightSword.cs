using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastKnightSword : KeywordMain
{
    private void Awake()
    {
        SetKeywordColor(RED);
        keywordDamage = 2;

    }
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.repeatStack *= (2);
        sentence.damage += (keywordDamage);
    }

    public override void Check(KeywordSup keywordSup)
    {

    }
}
