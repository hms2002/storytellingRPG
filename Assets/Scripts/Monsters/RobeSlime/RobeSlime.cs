using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobeSlime : Monster
{
    private void Awake()
    {
        MAX_HP = 90;
        hp = MAX_HP;
        encounterText = "로브를 뒤집어 쓴 슬라임이 나타났다.\n스치기만 해도 중독에 걸릴 것 같다.";
    }
    private void Start()
    {
        charactorState.AddState(StateType.mana, 10);
    }
    public override void StartTurn()
    {
        charactorState.AddState(StateType.mana, 3);
        base.StartTurn();
    }
}
