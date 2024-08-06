using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdShark_Predator : KeywordMain
{
    // Start is called before the first frame update
    void Awake()
    {
        keywordName = "포식자";
        SetKeywordColor(R);
        keywordTension = 10;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {

        caster.tension += keywordTension;
        target.damage += keywordDamage;
    }

    public override void CanUseCheck(Actor caster, Actor target)
    {
        if (target.beforePlayerDamage > 0)
        {
            isCanUse = false;
            keywordDamage = target.beforePlayerDamage;
        }
        else
        {
            isCanUse = true;
            keywordDamage = 0;
        }
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}
