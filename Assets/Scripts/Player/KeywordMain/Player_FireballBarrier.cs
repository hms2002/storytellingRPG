using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FireballBarrier : KeywordMain
{
    private void Awake()
    {
        keywordName = "불덩이 장벽";
        isPlayerKeyword = true;
        SetKeywordColor(B);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += target.charactorState.GetStateStack(StateType.burn) / 2;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
