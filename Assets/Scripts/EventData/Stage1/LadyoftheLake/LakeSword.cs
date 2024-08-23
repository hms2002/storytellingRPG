using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeSword : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.dmgList.Add(keywordDamage);
        caster.hp += keywordHeal;
    }

    public override void Check(KeywordSup keywordSup)
    {

    }
}
