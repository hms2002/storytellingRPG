using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EffectManager;
using static Keyword;

public class BarnacleCrayfish_ScatterBarnacles : KeywordMain
{
    [Header("따개비 뿌리기 키워드 일회성 약화 수치")]
    [SerializeField] private int oneTimeReductionControl = 7;

    // Start is called before the first frame update
    void Awake()
    {
        keywordName = "따개비 뿌리기";
        SetKeywordColor(Y);
        keywordTension = -30;
        effectTarget = EffectTarget.target;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateDatabase.stateDatabase.oneTimeReduction, oneTimeReductionControl);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
