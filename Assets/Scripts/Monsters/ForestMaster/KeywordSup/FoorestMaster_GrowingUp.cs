using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoorestMaster_GrowingUp : KeywordSup
{
    private void Awake()
    {
        keywordName = "자라나는";

        SetKeywordColor(R);
        keywordTension = -10;
        keywordDamage = 4;
        debuffStack = 1;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        caster.charactorState.AddState(StateType.multiplication, debuffStack);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
