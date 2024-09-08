using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : KeywordMain
{
    private void Awake()
    {
        keywordName = "점프스케어";

        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        int targetFearStack = target.charactorState.GetStateStack(StateType.fear);
        caster.dmgList.Add(keywordDamage * targetFearStack);
        caster.tension += keywordTension * targetFearStack;
        target.charactorState.ResetState(StateType.fear);
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}
