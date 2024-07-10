using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastKnightSword : KeywordMain
{
    int attackNum = 2;
    private void Awake()
    {
        SetKeywordColor(RED);
        keywordDamage = 2;
    }
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.SetXRepeat(attackNum);
        sentence.DamageControl(keywordDamage);
    }

    public override void Check(KeywordSup keywordSup)
    {

    }
}
