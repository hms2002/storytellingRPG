using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_PoisoningRoseShield : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "독장미 방패";
        SetKeywordColor(B);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;
        target.charactorState.AddState(StateType.venom, debuffStack);
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
