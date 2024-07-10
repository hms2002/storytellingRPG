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
        Debug.Log("Fist �ߵ�");
        sentence.DamageControl(keywordDamage);
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
