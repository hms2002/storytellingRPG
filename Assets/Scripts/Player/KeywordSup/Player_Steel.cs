using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Steel : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "강철의";
        SetKeywordColor(B);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
            target.charactorState.AddState(StateType.reduction, debuffStack);
            caster.protect += keywordProtect;
    }

    public override void Check(KeywordMain _keywordMain) { }
}
