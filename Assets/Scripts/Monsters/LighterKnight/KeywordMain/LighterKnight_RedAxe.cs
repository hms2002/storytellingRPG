using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterKnight_RedAxe : KeywordMain
{
    private void Awake()
    {
        keywordName = "붉은 도끼";

        SetKeywordColor(R);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {

        caster.damage += keywordDamage;
        caster.charactorState.AddState(StateType.burn, debuffStack);
        target.charactorState.AddState(StateType.burn, debuffStack);
        caster.tension += keywordTension;
    }
}
