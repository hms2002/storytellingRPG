using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ShieldCharge : KeywordMain
{
    private void Awake()
    {
        keywordName = "방패 돌격";
        isPlayerKeyword = true;
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        if(caster.protect >= 4)
        {
            caster.protect -= 4;
            caster.damage += 8;
            caster.afterAttackDel += (caster, target) =>
            {
                target.charactorState.AddState(StateType.weaken, 2);
            };
        }
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
