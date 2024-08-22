using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_PotionOfGhosted : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "귀신들린 포션";
        SetKeywordColor(D);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateType.fear, debuffStack);
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
