using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Davythulhu_Calling : KeywordMain
{
    private void Awake()
    {
        keywordName = "부름";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateType.fear, debuffStack);
        caster.tension += keywordTension;
    }
}
