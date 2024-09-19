using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionNero : Monster
{
    Animator anim;

    private void Awake()
    {
        MAX_HP = 90;
        hp = MAX_HP;
        encounterText = "강한 마력을 내뿜고 있는 조그만 검은 고양이가 나타났다.";

        anim = GetComponent<Animator>();
    }

    public override void Action(Actor target)
    {
        base.Action(target);

        if (charactorState.GetStateStack(StateType.faint) == 0)
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Down", false);
        }
        else
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Down", true);
        }
            
    }

    public override void BeforeAction()
    {
        base.BeforeAction();
        if (charactorState.GetStateStack(StateType.faint) == 0)
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Down", false);
        }
        else
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Down", true);
        }

    }
}
