using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdOre_ShardSpread : KeywordMain
{
    private void Awake()
    {
        keywordName = "파편 확산";
        SetKeywordColor(R);
        keywordTension = 32;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.dmgList.Add(keywordDamage);

        caster.charactorState.ReductionByValue(StateType.ore, buffStack);

        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}