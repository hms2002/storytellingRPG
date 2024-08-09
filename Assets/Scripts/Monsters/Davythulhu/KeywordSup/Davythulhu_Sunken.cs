using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Davythulhu_Sunken : KeywordSup
{
    private void Awake()
    {
        keywordName = "가라앉은";

        SetKeywordColor(R);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage = keywordDamage + target.charactorState.GetStateStack(StateType.fear) * keywordDamage;
        caster.tension += keywordTension;
    }
}
