using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Betrayal : KeywordMain
{
    [Header("배신감 키워드 약화 수치")]
    [SerializeField] private int oneTimeReductionControl = 3;

    // Start is called before the first frame update
    void Awake()
    {
        keywordName = "배신감";
        SetKeywordColor(Y);
        keywordTension = 12;
        keywordProtect = 0;
        effectTarget = EffectTarget.target;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        if(target.protect != 0)
        {
            target.protect = 0;
        }
        else
        {
            target.charactorState.AddState(StateDatabase.stateDatabase.oneTimeReduction, oneTimeReductionControl);
        }

        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
        
    }
}
