using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Blazing : KeywordSup
{
    [Header("자신에게 부여하는 화염스택")]
    [SerializeField]
    private int selfStack;
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(Y);
        effectTarget = EffectTarget.target;

        keywordName = "타오르는";
        effectType = EffectManager.EffectType.Flame;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateDatabase.stateDatabase.burn, debuffStack);
        caster.charactorState.AddState(StateDatabase.stateDatabase.burn, selfStack);
    }

    public override void Check(KeywordMain _keywordMain) { }
}
