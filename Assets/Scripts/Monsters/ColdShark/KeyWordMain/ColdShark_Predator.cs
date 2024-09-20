using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdShark_Predator : KeywordMain
{
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
        caster.dmgList.Add(keywordDamage);
    }

    public override void CanUseCheck(Actor caster, Actor target)
    {
        if (caster.beforeDamage == 0)
        {
            isCanUse = false;
            keywordDamage = 0;
        }
        else
        {
            isCanUse = true;
            keywordDamage = caster.beforeDamage;
        }
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}
