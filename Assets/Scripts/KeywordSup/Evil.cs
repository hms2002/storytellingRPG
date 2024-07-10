using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evil : KeywordSup
{
    private void Awake()
    {
        SetKeywordColor(RED);
        debuffType = "Weaken";
        debuffStack = 2;
    }

    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.weakenStack += debuffStack;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
