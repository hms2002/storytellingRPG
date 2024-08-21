using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AttackDeffenceStraddle : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "공방 협차";
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.dmgList.Add(keywordDamage);
        caster.charactorState.AddState(StateType.evasion, buffStack);
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
