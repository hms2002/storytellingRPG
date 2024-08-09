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
        target.Damaged(caster, 6);
        target.Damaged(caster, 4);
        target.Damaged(caster, 2);
        target.Damaged(caster, 1);
        caster.tension += keywordTension;
    }
}
