using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EffectManager;
using static Keyword;

public class BarnacleCrayfish_Stiff : KeywordSup
{
    [Header("딱딱한 키워드 보호 수치")]
    [SerializeField] private int protecte = 5;
    [Header("딱딱한 키워드 일회성 강화 수치")]
    [SerializeField] private int amountOfReinforce = 3;

    // Start is called before the first frame update
    void Awake()
    {
        keywordName = "딱딱한";
        SetKeywordColor(B);
        keywordTension = 8;
        keywordProtect = protecte;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateDatabase.stateDatabase.reinforce, amountOfReinforce);
        caster.protect += keywordProtect;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
