using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdOre : Monster
{
    [Header("광석 스택 초기값 설정")]
    [SerializeField] private int initOreStack = 25;

    private AdOre()
    {
        MAX_HP = 100;
        hp = MAX_HP;
        charactorState.AddState(StateType.ore, 25);
        encounterText = "대부분의 젊은이들이, 그들 앞의 존재를 알지 못한 채 채광을 계속했다.";
    }
}
