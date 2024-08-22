using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_VenomPotion : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "맹독 포션";
        SetKeywordColor(D);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.venom, debuffStack);
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
