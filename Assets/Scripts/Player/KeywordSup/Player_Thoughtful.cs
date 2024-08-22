using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Thoughtful : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "심사숙고한";
        SetKeywordColor(B);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;
        caster.charactorState.AddState(StateType.oneTimeReduction, debuffStack);
    }

    public override void Check(KeywordMain _keywordMain) { }
}
