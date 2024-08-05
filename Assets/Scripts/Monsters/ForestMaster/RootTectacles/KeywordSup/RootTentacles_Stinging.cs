using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootTentacles_Stinging : KeywordSup
{
    RootTectacles rootTectacles;
    private void Awake()
    {
        keywordName = "따가운";

        SetKeywordColor(Y);
        keywordTension = 8;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        if (caster is RootTectacles)
        {
            rootTectacles = (RootTectacles)caster;

            rootTectacles.tentacleAttackDamage += 1;
        }
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordSup)
    {

    }
}
