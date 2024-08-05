using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoorestMaster_Natural : KeywordSup
{
    ForestMaster forestMaster;
    private void Awake()
    {
        keywordName = "자연의";

        SetKeywordColor(Y);
        keywordTension = 16;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        if (caster is ForestMaster)
        {
            forestMaster = (ForestMaster)caster;
            forestMaster.rootTectacle.charactorState.AddState(StateType.tentacleCondolidation, 1);
            caster.tension += keywordTension;
        }
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void CanUseCheck(Actor caster, Actor target) 
    {

        if (!(caster is ForestMaster)) return;
        
        forestMaster = (ForestMaster)caster;

        if(forestMaster.rootTectacle == null)
            isCanUse = false;
        else
            isCanUse = true;
    }
}
