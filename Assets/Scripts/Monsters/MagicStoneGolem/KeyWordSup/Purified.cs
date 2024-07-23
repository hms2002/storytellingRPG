using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purified : KeywordSup
{
    private void Awake()
    {
        keywordName = "정화된";
        SetKeywordColor(B);
        keywordDamage = 3;
        keywordTension = -4;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage * caster.charactorState.DeleteAllDebuff();
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
