using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Melting : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(Y);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.venom, debuffStack);
    }

    public override void Check(KeywordMain keywordMain)
    {

    }
}
