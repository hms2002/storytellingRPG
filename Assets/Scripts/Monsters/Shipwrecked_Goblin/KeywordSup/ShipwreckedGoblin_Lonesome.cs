using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipwreckedGoblin_Lonesome : KeywordSup
{
    private void Awake()
    {
        keywordName = "쓸쓸한";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateType.weaken, debuffStack);
        caster.tension += keywordTension;
    }
}
