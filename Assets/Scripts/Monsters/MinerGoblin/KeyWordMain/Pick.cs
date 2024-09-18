using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : KeywordMain
{
    void Awake()
    {
        keywordName = "곡괭이";
        SetKeywordColor(R);
        keywordTension = -8;
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
