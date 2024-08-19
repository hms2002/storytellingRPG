using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceMagician_HexaDice : KeywordMain
{
    private void Awake()
    {
        keywordName = "육면체 주사위";

        SetKeywordColor(B);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        for (int i = 0; i < 2; i++)
        {
            caster.protect += Random.Range(1, 7);
        }

        caster.tension += keywordTension;
    }
}
