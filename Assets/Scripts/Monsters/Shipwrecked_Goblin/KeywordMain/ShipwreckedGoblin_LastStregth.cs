using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipwreckedGoblin_LastStregth : KeywordMain
{
    private void Awake()
    {
        keywordName = "마지막 힘";

        SetKeywordColor(R);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.dmgList.Add(keywordDamage);
        int debuffcnt = caster.charactorState.AllDebuffStack();
        if (debuffcnt > 30) debuffcnt = 30;
        caster.dmgList.Add(debuffcnt);
        caster.tension += keywordTension;
    }
}
