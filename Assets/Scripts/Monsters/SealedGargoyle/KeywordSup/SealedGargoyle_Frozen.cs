using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealedGargoyle_Frozen : KeywordSup
{
    private void Awake()
    {
        keywordName = "굳어있는";

        SetKeywordColor(R);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        caster.tension += keywordTension;
    }
}
