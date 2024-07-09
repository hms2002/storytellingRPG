using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : KeywordSup
{
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.ReapeatControl(1);
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
