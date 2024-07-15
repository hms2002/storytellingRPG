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
        sentence.damage += caster.protect;
    }

    public override void Check(KeywordMain _keywordMain)
    {
        if(_keywordMain.keywordProtect > 0)
        {

        }
    }
}
