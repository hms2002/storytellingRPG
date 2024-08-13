using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balshroom_SporeClouds : KeywordMain
{
    private void Awake()
    {
        keywordName = "포자 구름";

        SetKeywordColor(B);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {

        caster.protect += keywordProtect 
            + target.charactorState.GetStateStack(StateType.redSpore)*2;
        caster.tension += keywordTension;
    }
}
