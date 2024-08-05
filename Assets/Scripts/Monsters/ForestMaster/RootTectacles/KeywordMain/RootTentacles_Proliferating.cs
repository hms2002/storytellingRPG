using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootTentacles_Proliferating : KeywordMain
{
    RootTectacles rootTectacles;
    private void Awake()
    {
        keywordName = "증식하는";

        SetKeywordColor(Y);
        keywordTension = -10;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        if (caster is RootTectacles)
        {
            rootTectacles = (RootTectacles)caster;

            CanUseCheck(caster, target);
            if (isCanUse)
            {
                rootTectacles.forestMaster.charactorState.AddState(StateType.multiplication, 1);
            }
        }
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void CanUseCheck(Actor caster, Actor target)
    {
        if (caster is RootTectacles)
        {
            rootTectacles = (RootTectacles)caster;
            if (rootTectacles.forestMaster != null) isCanUse = true;
            else isCanUse = false;
        }
    }
}
