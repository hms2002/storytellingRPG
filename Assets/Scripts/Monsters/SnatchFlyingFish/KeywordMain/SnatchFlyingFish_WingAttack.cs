using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnatchFlyingFish_WingAttack : KeywordMain
{
    private void Awake()
    {
        keywordName = "날개치기";
        SetKeywordColor(R);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        caster.tension += keywordTension;
    }

}
