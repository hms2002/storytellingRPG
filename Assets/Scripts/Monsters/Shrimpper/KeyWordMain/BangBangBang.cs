using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangBangBang : KeywordMain
{
    private void Awake()
    {
        keywordName = "탕탕탕탕탕!";
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.tension += keywordTension * caster.charactorState.GetStateStack(StateType.ammunition);
        caster.damage += keywordDamage;
        caster.repeatStack = caster.charactorState.GetStateStack(StateType.ammunition);
        caster.charactorState.ResetState(StateType.ammunition);
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
