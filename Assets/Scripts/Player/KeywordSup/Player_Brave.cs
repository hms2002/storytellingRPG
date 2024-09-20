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
        if (caster.protect >= 2)
        {
            caster.dmgList.Plus(keywordDamage);

            target.charactorState.AddState(StateType.oneTimeReduction, 2);
        }
        else
            caster.dmgList.Plus(keywordDamage);

    }

    public override void Check(KeywordMain _keywordMain) { }
}
