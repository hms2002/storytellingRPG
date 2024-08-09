using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Davythulhu_TentacleAttack : KeywordMain
{
    private void Awake()
    {
        keywordName = "문어발 일격";

        SetKeywordColor(R);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage = keywordDamage;
        target.charactorState.AddState(StateType.fear, debuffStack);
        caster.tension += keywordTension;
    }
}
