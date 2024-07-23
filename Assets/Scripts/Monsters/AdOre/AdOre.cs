using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdOre : Monster
{
    [Header("광석 스택 초기값 설정")]
    [SerializeField] private int initOreStack = 25;

    void Start()
    {
        MAX_HP = 100;
        hp = MAX_HP;
        charactorState.AddState(StateType.ore, initOreStack);
    }
}
