using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSword : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.dmgList.Add(7);
    }

    public override void Check(KeywordSup _keywordSup) { }
}
