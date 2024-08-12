using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleGrip_Magical : KeywordSup
{
    private void Awake()
    {
        keywordName = "마력의";

        SetKeywordColor(R);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        if (!target.charactorState.vampire.Contains(caster))
            target.charactorState.vampire.Add(caster);
        caster.damage += keywordDamage;
        caster.tension += keywordTension;
    }
}
