using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleGrip_Sharp : KeywordSup
{
    private void Awake()
    {
        keywordName = "날카로운";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        if (!target.charactorState.vampire.Contains(caster))
            target.charactorState.vampire.Add(caster);
        caster.charactorState.AddState(StateType.weaken, debuffStack);
        caster.tension += keywordTension;
    }
}
