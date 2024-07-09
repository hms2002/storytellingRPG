using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pike : KeywordSup
{
    private void Awake()
    {
        SetKeywordColor(BLUE);
    }
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.PikeControl(3);
    }
    public override void Check(KeywordMain _keywordMain)
    {
        throw new System.NotImplementedException();
    }
}
