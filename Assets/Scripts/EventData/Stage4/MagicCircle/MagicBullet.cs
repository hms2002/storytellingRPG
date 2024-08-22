using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.dmgList.Add(keywordDamage, true);
    }

    public override void Check(KeywordSup keywordSup)
    {

    }
}
