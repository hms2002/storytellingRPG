using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionNero_PillarFlame : KeywordMain
{
    [Header("화염 기둥! 키워드 데미지 수치")]
    [SerializeField] private int damage = 10;
    [Header("화염 기둥! 기절 수치")]
    [SerializeField] private int faintTime = 1;
    [Header("화염 기둥 ! 화상 수치")]
    [SerializeField] private int BurnTime = 5;

    private void Awake()
    {
        keywordName = "화염 기둥!";
        SetKeywordColor(R);
        keywordTension = 7;
        keywordDamage = damage;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.tension += keywordTension;
        target.damage += damage;
        target.charactorState.AddState(StateDatabase.stateDatabase.burn, BurnTime);
        caster.charactorState.AddState(StateDatabase.stateDatabase.faint, faintTime);
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}
