using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balshroom_SporeBunch : KeywordMain
{
    private void Awake()
    {
        keywordName = "포자 뭉치";

        SetKeywordColor(B);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect
            + target.charactorState.GetStateStack(StateType.blueSpore);
        caster.tension += keywordTension;
    }
}
