using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ori_Smash : KeywordMain
{
    private void Awake()
    {
        keywordName = "분쇄";

        SetKeywordColor(RED);
        keywordTension = -10;
        keywordDamage = 6;
    }
    
    public override void Execute(Actor caster, Actor target)
    {
        target.damage += (int)Random.Range(keywordDamage, 9);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}
