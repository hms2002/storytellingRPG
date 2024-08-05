using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestMaster_ProtectionForest : KeywordMain
{
    ForestMaster forestMaster;
    private void Awake()
    {
        keywordName = "숲의 가호";

        SetKeywordColor(Y);
        keywordTension = -10;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        if (caster is ForestMaster)
        {
            forestMaster = (ForestMaster)caster;
            if(forestMaster.rootTectacle == null)
            {
                caster.charactorState.DeleteAllDebuff();
            }
            else
            {
                if(caster.charactorState.GetStateStack(StateType.multiplication) >= 2)
                {
                    caster.charactorState.DeleteAllDebuff();
                    forestMaster.rootTectacle.charactorState.DeleteAllDebuff();
                    forestMaster.rootTectacle.hp += 15;
                }
                else
                {
                    forestMaster.rootTectacle.hp += 10;
                }
            }
        }
        caster.tension += keywordTension;
    }




    public override void Check(KeywordSup _keywordSup)
    {

    }
}
