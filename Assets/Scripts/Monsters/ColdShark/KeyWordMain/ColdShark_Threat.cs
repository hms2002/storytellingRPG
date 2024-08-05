using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdShark_Threat : KeywordMain
{
    [Header("위협 키워드 약화 수치(상대)")]
    [SerializeField] private int oneTimeReductionControl = 8;
    [Header("위협 키워드 쉴드 수치(자신)")]
    [SerializeField] private int protecte = 5;

    void Awake()
    {
        keywordName = "위협";
        SetKeywordColor(Y);
        keywordTension = 8;
        keywordProtect = protecte;
        //이 아래 이펙트 효과 수정 필요.
        effectTarget = EffectTarget.target;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateDatabase.stateDatabase.oneTimeReduction, oneTimeReductionControl);
        caster.protect += keywordProtect;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
