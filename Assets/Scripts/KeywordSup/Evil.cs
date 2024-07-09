using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evil : KeywordSup
{
    private void Awake()
    {
        SetKeywordColor(RED);
    }
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        caster.weakenAttack = true;
        sentence.WeakenControl(1);
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }
}
