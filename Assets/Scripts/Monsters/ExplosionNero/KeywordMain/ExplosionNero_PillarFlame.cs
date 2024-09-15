using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionNero_PillarFlame : KeywordMain
{
    private void Awake()
    {
        keywordName = "화염 기둥!";
        SetKeywordColor(R);
        keywordTension = 7;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.tension += keywordTension;
        caster.dmgList.Add(keywordDamage);
        target.dmgList.Add(25);
        target.charactorState.AddState(StateDatabase.stateDatabase.burn, debuffStack);
        caster.charactorState.AddState(StateDatabase.stateDatabase.faint, 1);
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}
