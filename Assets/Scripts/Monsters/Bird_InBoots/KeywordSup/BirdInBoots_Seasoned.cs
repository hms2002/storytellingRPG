using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdInBoots_Seasoned : KeywordSup
{

    private void Awake()
    {
        keywordName = "노련한";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.counterAttack, buffStack);
        caster.charactorState.AddState(StateType.reduction, debuffStack);
        caster.tension += keywordTension;
    }
}
