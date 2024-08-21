using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Powerful : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "강력한";
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        caster.charactorState.AddState(StateType.weaken, debuffStack);
    }

    public override void Check(KeywordMain _keywordMain) { }
}
