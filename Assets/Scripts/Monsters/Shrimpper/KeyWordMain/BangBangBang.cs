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
        for(int i =0; i <= caster.charactorState.GetStateStack(StateType.ammunition); i++)
        {
            caster.dmgList.Add(keywordDamage);
        }
        caster.charactorState.ResetState(StateType.ammunition);
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}
