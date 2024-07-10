using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharp : KeywordSup
{
    private void Awake()
    {
        SetKeywordColor(RED);
        keywordDamage = 2;
    }
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.DamageControl(keywordDamage);
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }
}
