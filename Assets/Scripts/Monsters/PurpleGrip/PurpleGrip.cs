using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleGrip : Monster
{

    private void Awake()
    {
        MAX_HP = 80;
        hp = MAX_HP;
        encounterText = "보랏빛이 나는 구체를 쥐고 있는 손아귀가 나타났다";
    }
    private void Start()
    {
        charactorState.AddState(StateType.absorption, 1);
    }
}
