using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Sloppy : KeywordSup
{
    [Header("부여되는 일회성 약화 수치")]
    [SerializeField] private int oneTimeReductionControl = 1;


    private void Awake()
    {
        keywordName = "엉성한";
        SetKeywordColor(Y);
        keywordTension = -18;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateDatabase.stateDatabase.oneTimeReduction, oneTimeReductionControl);

        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
