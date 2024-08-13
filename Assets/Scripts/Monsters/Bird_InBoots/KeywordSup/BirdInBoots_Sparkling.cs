using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdInBoots_Sparkling : KeywordSup
{

    private void Awake()
    {
        keywordName = "번뜩이는";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.reinforce, buffStack);
        caster.charactorState.AddState(StateType.weaken, debuffStack);
        caster.tension += keywordTension;
    }
}
