using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : KeywordMain
{
    private void Awake()
    {
        SetKeywordColor(RED);
        keywordDamage = 1;
    }
    public override void Execute(Actor self, Actor target, Sentence sentence)
    {
        int fistDamage = 1;
        sentence.damage += fistDamage;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
