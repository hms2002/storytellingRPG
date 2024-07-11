using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twingkle : KeywordSup
{
    private void Awake()
    {
        SetKeywordColor(BLUE);
        keywordProtect = 5;
    }

    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.protect = keywordProtect;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
