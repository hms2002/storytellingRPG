using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EffectManager;
using static Keyword;

public class ExplosionNero_Yay : KeywordSup
{
    [Header("부여되는 강화 수치 제어")]
    [SerializeField] private int amountOfReinforce = 5;


    private void Awake()
    {
        keywordName = "이얍!";
        SetKeywordColor(Y);
        keywordTension = -7;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateDatabase.stateDatabase.reinforce, amountOfReinforce);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
