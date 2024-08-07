using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnatchFlyingFish_Seizure : KeywordMain
{
    private void Awake()
    {
        keywordName = "강탈";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        
        if(caster is SnatchFlyingFish)
        {
            SnatchFlyingFish fish = (SnatchFlyingFish)caster;
            fish.StealGold(target, debuffStack);
        }
    }
}
