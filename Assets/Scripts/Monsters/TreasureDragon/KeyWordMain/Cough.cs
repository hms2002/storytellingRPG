using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cough : KeywordMain
{
    [Header("랜덤 데미지 제어")]
    [SerializeField] private int maxRange = 30;
    [SerializeField] private int minRange = 20;

    private void Awake()
    {
        keywordName = "기침(브레스)";
        SetKeywordColor(R);
        keywordDamage = Random.Range(minRange, maxRange);
        debuffStack = 5;
        debuffType = "Burn";
        keywordTension = 41;
        Init();
    }
    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        target.charactorState.AddState(StateDatabase.stateDatabase.burn, debuffStack);
        caster.tension += keywordTension;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
