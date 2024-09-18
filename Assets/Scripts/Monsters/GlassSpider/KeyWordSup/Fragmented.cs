using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragmented : KeywordSup
{
    private void Awake()
    {
        keywordName = "파편화된";
        SetKeywordColor(R);
        keywordTension = 5;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.dmgList.Plus(caster.charactorState.GetStateStack(StateType.glassPragment) * 3);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
