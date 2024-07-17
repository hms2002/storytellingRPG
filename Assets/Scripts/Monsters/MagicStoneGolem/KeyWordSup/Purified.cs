using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purified : KeywordSup
{
    private void Awake()
    {
        keywordName = "정화된";
        SetKeywordColor(BLUE);
        keywordDamage = 3;
        keywordTension = -4;
    }

    public override void Execute(Actor caster, Actor target)
    {
        for (int i = 0; i < caster.debuffList.Length; i++)
        {
            if(caster.debuffList[i] > 0)
            {
                caster.debuffList[i] = 0;
                caster.damage += keywordDamage;
            }
        }
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
