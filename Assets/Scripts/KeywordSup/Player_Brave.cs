using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Brave : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "용맹한";
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        if (caster.protect >= 3)
        {
            caster.dmgList.Plus(keywordDamage);
            caster.protect -= 3;

            target.charactorState.AddState(StateType.oneTimeReduction, 2);
        }
    }

    public override void Check(KeywordMain _keywordMain) { }

}
