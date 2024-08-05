using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootTentacles_NutrientDelivery : KeywordMain
{
    RootTectacles rootTectacles;
    private void Awake()
    {
        keywordName = "양분 전달";

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
                rootTectacles.forestMaster.charactorState.ReductionByValue(StateType.multiplication, 1);
                rootTectacles.forestMaster.protect += 5;
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
            if (rootTectacles.forestMaster == null)
            {
                isCanUse = false;
                return;
            }
            if (rootTectacles.forestMaster.charactorState
                .GetStateStack(StateType.multiplication) >= 1)
            {
                isCanUse = true;
                return;
            }
        }
        isCanUse = false;
    }
}