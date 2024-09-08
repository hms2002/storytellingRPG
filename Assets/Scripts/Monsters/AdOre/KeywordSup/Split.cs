using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : KeywordSup
{
    private void Awake()
    {
        keywordName = "쪼개진";
        SetKeywordColor(R);
        keywordTension = -8;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.dmgList.Plus(keywordDamage);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
