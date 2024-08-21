using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Davythulhu_CaptainHook : KeywordMain
{
    private void Awake()
    {
        keywordName = "선장의 갈고리";

        SetKeywordColor(R);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.dmgList.Add(keywordDamage);
        caster.tension += keywordTension;
    }
}
