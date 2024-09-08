using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Composure : KeywordMain
{
    private void Awake()
    {
        keywordName = "평정심";
        SetKeywordColor(Y);
        Init(); 
    }

    public override void Execute(Actor caster, Actor target)
    {

    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
