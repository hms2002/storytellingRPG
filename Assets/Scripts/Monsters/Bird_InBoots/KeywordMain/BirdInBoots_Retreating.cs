using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdInBoots_Retreating : KeywordMain
{
    private void Awake()
    {
        keywordName = "후퇴!!";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.secession, buffStack);
        caster.tension += keywordTension;
    }
}
