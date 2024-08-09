using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Davythulhu_Deep : KeywordSup
{

    private void Awake()
    {
        keywordName = "심해의";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateType.fear, debuffStack);
        caster.tension += keywordTension;
    }
}
