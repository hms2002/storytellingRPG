using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pike : KeywordSup
{
    private void Awake()
    {
        SetKeywordColor(BLUE);
        keywordDamage = 3;
    }
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.pike += (3);
    }
    public override void Check(KeywordMain _keywordMain)
    {

    }
}
