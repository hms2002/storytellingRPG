using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipwreckedGoblin_Alive : KeywordSup
{
    private void Awake()
    {
        keywordName = "살아있는";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.oneTimeReinforce, buffStack);
        caster.tension += keywordTension;
    }
}
