using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ori_Fregments : KeywordMain
{
    private void Awake()
    {
        keywordName = "몸통 박치기";

        SetKeywordColor(R);
        keywordTension = -10;
        keywordDamage = 3;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}
