using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipwreckedGoblin_GibsonThrow : KeywordMain
{
    private void Awake()
    {
        keywordName = "깁슨 던지기";

        SetKeywordColor(R);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        caster.charactorState.AddState(StateType.weaken, debuffStack);
        caster.charactorState.AddState(StateType.reduction, debuffStack);
        caster.tension += keywordTension;
        ShipwreckedGoblin goblin = (ShipwreckedGoblin)caster;
        goblin.ThrowGibson();
    }
}
