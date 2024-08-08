using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EffectManager;
using static Keyword;

public class ExplosionNero_ChainExplosion : KeywordMain
{
    [Header("연쇄 폭팔! 키워드 데미지 수치")]
    [SerializeField] private int damage = 10;
    [Header("연쇄 폭팔! 기절 수치")]
    [SerializeField] private int faintTime = 1;
    [Header("연쇄 폭팔! 연쇄 공격 횟수")]
    [SerializeField] private int attackNum = 2;
    [Header("연쇄 폭팔! 키워드 자해 데미지 수치")]
    [SerializeField] private int selfDamage = 10;

    private void Awake()
    {
        keywordName = "연쇄 폭팔!";
        SetKeywordColor(R);
        keywordTension = 9;
        keywordDamage = damage;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.tension += keywordTension;

        caster.repeatStack *= attackNum;
        caster.damage += damage;
        caster.Damaged(caster, selfDamage);
        
        caster.charactorState.AddState(StateDatabase.stateDatabase.faint, faintTime);
    }

    public override void Check(KeywordSup _keywordSup)
    {
        
    }
}
