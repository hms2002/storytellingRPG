using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joyful : KeywordSup
{
    private void Awake()
    {
        SetKeywordColor(RED);
        keywordDamage = 5;
    }

    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.nextTurnDamage += keywordDamage;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
