using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdOre : Monster
{
    Animator anim;
    private AdOre()
    {
        MAX_HP = 100;
        hp = MAX_HP;
        encounterText = "대부분의 젊은이들이, 그들 앞의 존재를 알지 못한 채 채광을 계속했다.";
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        charactorState.AddState(StateType.ore, 1);
    }
    public override void Damaged(Actor attacker, int _damage)
    {
        base.Damaged(attacker, _damage);
        if (charactorState.GetStateStack(StateType.ore) == 0)
            anim.SetTrigger("Break");
        else
            anim.SetTrigger("Restore");
    }
}
