using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceMagician_QuadraeDice : KeywordMain
{
    private void Awake()
    {
        keywordName = "사면체 주사위";

        SetKeywordColor(R);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        for(int i = 0; i < 3; i++)
        {
            caster.dmgList.Add(Random.Range(1, 5));
        }
        
        caster.tension += keywordTension;
    }
}
