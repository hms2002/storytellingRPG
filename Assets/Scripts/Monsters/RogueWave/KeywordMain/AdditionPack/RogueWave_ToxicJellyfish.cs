using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueWave_ToxicJellyfish : KeywordMain
{
    private void Awake()
    {
        keywordName = "독성 해파리";

        SetKeywordColor(Y);
        Init();
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.venom, debuffStack);
        caster.tension += keywordTension;
    }
}
