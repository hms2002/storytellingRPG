using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealedGargoyle_WingBlast : KeywordMain
{
    private void Awake()
    {
        keywordName = "날개 돌풍";

        SetKeywordColor(R);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.dmgList.Add(6);
        caster.dmgList.Add(4);
        caster.dmgList.Add(2);
        caster.dmgList.Add(1);
        caster.tension += keywordTension;
    }
}
