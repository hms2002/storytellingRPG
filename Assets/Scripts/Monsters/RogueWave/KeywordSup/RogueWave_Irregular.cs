using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueWave_Irregular : KeywordSup
{
    private void Awake()
    {
        keywordName = "불규칙한";

        SetKeywordColor(B);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        int randVal = Random.Range(0, 2);
        if (randVal == 0)
            caster.charactorState.AddState(StateType.oneTimeReinforce, buffStack);
        else 
            caster.charactorState.AddState(StateType.oneTimeReduction, debuffStack);

        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordSup)
    {

    }
}
