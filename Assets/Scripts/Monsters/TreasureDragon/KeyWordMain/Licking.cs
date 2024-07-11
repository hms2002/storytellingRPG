using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liking : KeywordMain
{
    private void Awake()
    {
        SetKeywordColor(RED);
        keywordDamage = Random.Range(6, 10);
    }
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.damage += keywordDamage;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
