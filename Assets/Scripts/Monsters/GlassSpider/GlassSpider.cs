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
    

    private void Start()
    {
        hp = MAX_HP;
    }

    protected override int CalculateCounterAttackDamage(Actor FightBacker)
    {
        int counterDamage = base.CalculateCounterAttackDamage(FightBacker);
        FightBacker.charactorState.AddState(StateType.glassPragment, 1);
        return counterDamage;
    }
}
