using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rattling : KeywordSup
{
    private void Awake()
    {
        keywordName = "달그락거리는";

        SetKeywordColor(R);
        keywordTension = -10;
        keywordDamage = 3;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
