using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterKnight_Hot : KeywordSup
{
    private void Awake()
    {
        keywordName = "뜨거운";

        SetKeywordColor(B);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += target.charactorState.GetStateStack(StateType.burn) / 2;
        caster.tension += keywordTension;
    }
}
