using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biting : KeywordMain
{
    private void Awake()
    {
        keywordName = "물어뜯기";
        SetKeywordColor(R);
        keywordTension = 10;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.dmgList.Add(keywordDamage);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
