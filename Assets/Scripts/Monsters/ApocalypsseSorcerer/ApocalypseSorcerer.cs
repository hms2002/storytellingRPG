using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApocalypseSorcerer : Monster
{
    private void Awake()
    {
        MAX_HP = 300;
        hp = MAX_HP;
        encounterText = "종말을 알리는 불길한 주문이 시작된다.";
    }

    private void Start()
    {
        charactorState.AddState(StateType.mana, 30);
    }

    public override void StartTurn()
    {
        charactorState.AddState(StateType.mana, 3);
        charactorState.AddState(StateType.end, 1);
        base.StartTurn();
    }
}
