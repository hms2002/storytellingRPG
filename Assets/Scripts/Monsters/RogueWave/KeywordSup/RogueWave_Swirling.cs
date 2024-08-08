using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueWave_Swirling : KeywordSup
{
    private void Awake()
    {
        keywordName = "휘몰아치는";

        SetKeywordColor(R);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        target.charactorState.AddState(StateType.reduction, debuffStack);
        caster.tension += keywordTension;
    }
}
