using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassSpider : Monster
{
    GlassSpider()
    {
        encounterText = "";
        _MAX_HP = 50;
    }

    Animator anim;
    private void Start()
    {
        hp = MAX_HP;
        anim = GetComponent<Animator>();
    }

    protected override int CalculateCounterAttackDamage(Actor FightBacker)
    {
        int counterDamage = base.CalculateCounterAttackDamage(FightBacker);
        FightBacker.charactorState.AddState(StateType.glassPragment, 1);
        anim.SetTrigger("Break");
        return counterDamage;
    }
}
