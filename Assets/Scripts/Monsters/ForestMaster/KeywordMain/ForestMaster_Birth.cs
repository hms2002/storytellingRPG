using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestMaster_Birth : KeywordMain
{
    ForestMaster forestMaster;
    private void Awake()
    {
        keywordName = "탄생";

        SetKeywordColor(Y);
        keywordTension = 32;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        if (caster is ForestMaster)
        {
            forestMaster = (ForestMaster)caster;
            caster.charactorState.DeleteAllDebuff();
            forestMaster.SpawnRootTectacle();
            caster.tension += keywordTension;
        }
    }



    public override void CanUseCheck(Actor caster, Actor target) 
    {

        if (caster is ForestMaster)
        {
            forestMaster = (ForestMaster)caster;
            if (forestMaster.rootTectacle == null)
                isCanUse = true;
            else
                isCanUse = false; 
        }
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}
