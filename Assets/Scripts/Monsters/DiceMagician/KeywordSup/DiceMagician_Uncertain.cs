using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceMagician_Uncertain : KeywordSup
{
    private void Awake()
    {
        keywordName = "불확실한";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        int randomFlag = Random.Range(0, 2);
        if (randomFlag == 0)
            caster.charactorState.AddState(StateType.reduction, debuffStack);
        else
            caster.charactorState.AddState(StateType.reinforce, buffStack);

        caster.tension += keywordTension;
    }
}
