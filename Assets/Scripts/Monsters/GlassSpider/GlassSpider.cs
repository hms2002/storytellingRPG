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

    public override void Damaged(Actor attacker, int _damage)
    {
        if (_damage <= 0)
            return;
        int totalDamage = _damage;
        
        if (totalDamage > 0 && attacker != this)
        {
            charactorState.AddState(StateDatabase.stateDatabase.glassPragment, 1);
            attackCount = true;
        }
        int weakenStack = charactorState.GetStateStack(StateType.weaken);
        if (weakenStack > 0)
        {
            totalDamage += weakenStack;
            weakenStack -= 1;
        }

        if (protect > 0)
        {
            if (protect < totalDamage)
            {
                totalDamage -= protect;
                protect = 0;
            }
            else
            {
                protect -= totalDamage;
                totalDamage = 0;
            }
        }

        hp -= totalDamage;
    }
}
