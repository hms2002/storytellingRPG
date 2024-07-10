using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindSword : KeywordMain
{
    [SerializeField]private int stack = 1;
    private void Awake()
    {
        SetKeywordColor(RED);
        debuffStack = stack;
    }
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {

    }

    public override void Check(KeywordSup keywordSup)
    {
        if(keywordSup.debuffType == "Burn")
        {
            keywordSup.debuffStack += stack;
        }
        if(keywordSup.debuffType == "Weaken")
        {
            keywordSup.debuffStack += stack;
        }
    }
}
