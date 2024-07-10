using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossed : KeywordSup
{
    private void Awake()
    {
        SetKeywordColor(BLUE);
    }
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        Debug.Log("교차된 발동");

        sentence.DamageControl(caster.protect);
    }

    public override void Check(KeywordMain _keywordMain)
    {
        
    }
}
