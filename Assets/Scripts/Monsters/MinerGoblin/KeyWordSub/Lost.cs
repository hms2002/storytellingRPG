using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lost : KeywordSup
{
    // Start is called before the first frame update
    void Awake()
    {
        keywordName = "길을 잃은";
        SetKeywordColor(Y);
        keywordTension = 8;
        effectTarget = EffectTarget.target;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateDatabase.stateDatabase.oneTimeReduction, debuffStack);

        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
