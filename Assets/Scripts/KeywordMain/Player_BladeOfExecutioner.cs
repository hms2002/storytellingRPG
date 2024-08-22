using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BladeOfExecutioner : KeywordMain
{
    private void Awake()
    {
        keywordName = "처형자의 칼날";
        isPlayerKeyword = true;
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.dmgList.Add(keywordDamage);
        caster.charactorState.StackGainDouble(StateType.oneTimeReinforce);
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
