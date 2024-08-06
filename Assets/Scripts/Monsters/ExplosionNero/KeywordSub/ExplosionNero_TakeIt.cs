using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EffectManager;
using static Keyword;

public class ExplosionNero_TakeIt : KeywordSup
{
    [Header("부여되는 강화 수치 제어")]
    [SerializeField] private int amountOfReinforce = 13;


    private void Awake()
    {
        keywordName = "받아라!";
        SetKeywordColor(Y);
        keywordTension = 10;
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
