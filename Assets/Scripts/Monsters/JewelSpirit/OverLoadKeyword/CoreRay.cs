using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreRay : KeywordMain
{
    private void Awake()
    {
        keywordName = "코어 광선";
        SetKeywordColor(R);
        keywordTension = -10;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect -= keywordProtect;
        caster.dmgList.Add(keywordDamage);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
