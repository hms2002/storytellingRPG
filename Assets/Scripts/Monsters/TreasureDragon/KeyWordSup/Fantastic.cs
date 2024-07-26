using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fantastic : KeywordSup
{
    TrasureDragon trasureDragon;

    [Header("환상적인 키워드 데미지 수치 제어")]
    [SerializeField] private int damageFigures = 20;


    private void Awake()
    {
        keywordName = "환상적인";
        SetKeywordColor(Y);
        keywordTension = -11;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        trasureDragon = caster as TrasureDragon;
        trasureDragon.charactorState.ReductionByValue(StateType.treasureOfDragon, damageFigures);
        trasureDragon.trasureDamage += damageFigures;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
