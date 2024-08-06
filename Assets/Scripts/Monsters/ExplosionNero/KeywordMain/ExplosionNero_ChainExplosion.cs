using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EffectManager;
using static Keyword;

public class ExplosionNero_ChainExplosion : KeywordMain
{
    [Header("연쇄 폭팔! 키워드 데미지 수치")]
    [SerializeField] private int damage = 10;

    private void Awake()
    {
        keywordName = "연쇄 폭팔!";
        SetKeywordColor(Y);
        keywordTension = -8;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
        
    }
}
