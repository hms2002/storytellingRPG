using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingCat : KeywordMain
{
    private void Awake()
    {
        keywordName = "수레 밀치기";
        SetKeywordColor(R);
        keywordTension = 18;
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
