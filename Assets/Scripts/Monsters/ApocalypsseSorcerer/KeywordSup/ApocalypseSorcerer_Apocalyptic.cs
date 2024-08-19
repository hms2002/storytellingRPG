using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApocalypseSorcerer_Apocalyptic : KeywordSup
{

    private void Awake()
    {
        keywordName = "종말의";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        int apocalStack = caster.charactorState.GetStateStack(StateType.end);
        caster.charactorState.AddState(StateType.reinforce, apocalStack * 3);
        caster.charactorState.AddState(StateType.weaken, apocalStack * 2);
        caster.tension += keywordTension;
    }
}
