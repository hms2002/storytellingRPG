using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ShieldSlam : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "방패 밀치기";
        SetKeywordColor(Y);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        if(caster.protect >= 10)
        {
            caster.protect -= 10;
            target.charactorState.AddState(StateType.faint, 1);
        }   
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
