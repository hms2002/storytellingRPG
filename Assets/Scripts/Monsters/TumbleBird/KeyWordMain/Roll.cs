using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Roll : KeywordMain
{
    TumbleBird tumbleBird;

    [Header("구르기 랜덤 데미지 범위 제어")]
    [SerializeField] private int minDamage = 5;
    [SerializeField] private int maxDamage = 8;


    private void Awake()
    {
        keywordName = "구르기";
        SetKeywordColor(R);
        keywordDamage = UnityEngine.Random.Range(minDamage, maxDamage);
        debuffStack = 5;
        keywordTension = -6;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        tumbleBird = caster as TumbleBird;
        caster.damage += keywordDamage;

        // Enum 0부터 항목 갯수 - 1 중에 Random값 뽑기
        int randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TumbleBird.TumbleBirdBuffList)).Length);

        // 값 받아올 변수 선언
        TumbleBird.TumbleBirdBuffList buffEnum = TumbleBird.TumbleBirdBuffList.protect;
        foreach (TumbleBird.TumbleBirdBuffList i in Enum.GetValues(typeof(TumbleBird.TumbleBirdBuffList)))
        {
            // 반복마다 1씩 줄다가 0 되면 값 대입 후 break
            if (randomIndex == 0)
            {
                buffEnum = i;
                break;
            }
            randomIndex--;
        }

        // 보호는 charactorState에서 다루지 않기에 따로 올려준다.
        if ((StateType)buffEnum == StateType.protect)
            caster.protect += 5;
        else            
            caster.charactorState.AddState((StateType)buffEnum, 5);

        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
