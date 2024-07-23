using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdOre_ShardOverload : KeywordMain
{
    [Header("부여되는 일회성 강화 수치 제어")]
    [SerializeField] int amountOfOneTimeReinforce = 10;

    [Header("광석 스택 감소 수치 제어")]
    [SerializeField] int amountOfDecrease = 5;


    private void Awake()
    {
        keywordName = "파편 폭주";
        //SetKeywordColor(Y);
        keywordTension = 26;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.oneTimeReinforce, amountOfOneTimeReinforce);

        caster.charactorState.ReductionByValue(StateType.ore, amountOfDecrease);

        target.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}