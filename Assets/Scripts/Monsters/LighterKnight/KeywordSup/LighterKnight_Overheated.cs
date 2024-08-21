using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterKnight_Overheated : KeywordSup
{
    private void Awake()
    {
        keywordName = "과열된";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.Damaged(caster, 
            new DamageInfo(( caster.charactorState.GetStateStack(StateType.burn) / 2) * 3
            ));
        caster.charactorState.ResetState(StateType.burn);
        caster.tension += keywordTension;
    }
}
