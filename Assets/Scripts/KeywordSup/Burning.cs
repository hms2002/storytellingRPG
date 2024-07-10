using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burning : KeywordSup
{
    [SerializeField] private int stack = 2;
    private void Awake()
    {
        SetKeywordColor(RED);
        debuffType = "Burn";
        debuffStack = 2;
    }

    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.burnStack += (debuffStack);
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
