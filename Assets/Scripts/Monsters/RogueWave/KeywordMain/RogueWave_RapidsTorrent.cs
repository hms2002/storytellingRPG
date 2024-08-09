using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RogueWave_RapidsTorrent : KeywordMain
{
    RogueWave wave;
    private void Awake()
    {
        keywordName = "급류";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        wave = caster as RogueWave;
        caster.damage += keywordDamage;

        // Enum 0부터 항목 갯수 - 1 중에 Random값 뽑기
        int randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(RogueWave.stateList)).Length);

        // 값 받아올 변수 선언
        RogueWave.stateList buffEnum = RogueWave.stateList.protect;
        foreach (RogueWave.stateList i in Enum.GetValues(typeof(RogueWave.stateList)))
        {
            // 반복마다 1씩 줄다가 0 되면 값 대입 후 break
            if (randomIndex == 0)
            {
                buffEnum = i;
                break;
            }
            randomIndex--;
        }

        switch ((StateType)buffEnum)
        {
            case StateType.protect:
                caster.protect += 8;
                break;
            case StateType.weaken:
                caster.charactorState.AddState((StateType)buffEnum, 3);
                break;
            case StateType.reduction:
                caster.charactorState.AddState((StateType)buffEnum, 3);
                break;
            case StateType.reinforce:
                caster.charactorState.AddState((StateType)buffEnum, 3);
                break;
            case StateType.glassPragment:
                caster.charactorState.AddState((StateType)buffEnum, 1);
                break;
            default:
                Debug.LogError("인덱스 오버");
                break;
        }
        randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(RogueWave.stateList)).Length);
        buffEnum = RogueWave.stateList.protect;
        foreach (RogueWave.stateList i in Enum.GetValues(typeof(RogueWave.stateList)))
        {
            // 반복마다 1씩 줄다가 0 되면 값 대입 후 break
            if (randomIndex == 0)
            {
                buffEnum = i;
                break;
            }
            randomIndex--;
        }
        switch ((StateType)buffEnum)
        {
            case StateType.protect:
                target.protect += 8;
                break;
            case StateType.weaken:
                target.charactorState.AddState((StateType)buffEnum, 3);
                break;
            case StateType.reduction:
                target.charactorState.AddState((StateType)buffEnum, 3);
                break;
            case StateType.reinforce:
                target.charactorState.AddState((StateType)buffEnum, 3);
                break;
            case StateType.glassPragment:
                target.charactorState.AddState((StateType)buffEnum, 1);
                break;
            default:
                Debug.LogError("인덱스 오버");
                break;
        }

        caster.tension += keywordTension;
    }
}
