using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EffectManager;
using static Keyword;

public class Frightened : KeywordSup
{
    [Header("부여되는 일회성 약화 수치")]
    [SerializeField] private int oneTimeReductionControl = 4;

    private void Awake()
    {
        keywordName = "겁에 질린";
        SetKeywordColor(Y);
        keywordTension = -20;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateDatabase.stateDatabase.oneTimeReduction, oneTimeReductionControl);

        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
