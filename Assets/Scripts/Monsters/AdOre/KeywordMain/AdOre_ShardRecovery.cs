using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdOre_ShardRecovery : KeywordMain
{
    [Header("부여되는 광석 스택 수치 제어")]
    [SerializeField] int amountOfRecovery = 25;


    private void Awake()
    {
        keywordName = "파편 복구";
        SetKeywordColor(Y);
        keywordTension = 52;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.ore, amountOfRecovery);

        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}