using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evil : KeywordSup
{
    private void Awake()
    {
        keywordName = "악랄한";
        SetKeywordColor(R);
        debuffType = "Weaken";
        debuffStack = 2;
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateDatabase.stateDatabase.weaken, debuffStack);
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
