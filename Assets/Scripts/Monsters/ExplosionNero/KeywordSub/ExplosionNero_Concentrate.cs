using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionNero_Concentrate : KeywordSup
{
    [Header("부여되는 강화 수치 제어")]
    [SerializeField] private int amountOfReinforce = 5;


    private void Awake()
    {
        keywordName = "집중하고...";
        SetKeywordColor(Y);
        keywordTension = -8;
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
