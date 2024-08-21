using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_OilBottle : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "기름병";
        SetKeywordColor(D);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        if (target.charactorState.GetStateStack(StateType.burn) > 0)
            caster.damage += keywordDamage;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
