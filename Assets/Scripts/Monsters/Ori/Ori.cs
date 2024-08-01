using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ori : Monster
{
    Animator anim;
    private void Awake()
    {
        MAX_HP = 50;
        encounterText = "광물을 두드릴 때는 다시 한 번 생각해야 했다.";
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        charactorState.AddState(StateType.ore, 18);
    }

    public override void Damaged(Actor attacker, int _damage)
    {
        base.Damaged(attacker, _damage);
        if (charactorState.GetStateStack(StateType.ore) == 0)
            anim.SetTrigger("Break");
    }
}
