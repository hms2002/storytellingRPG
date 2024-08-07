using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnatchFlyingFish : Monster
{
    private void Awake()
    {
        MAX_HP = 50;
        hp = MAX_HP;
        encounterText = "아앗! 날치에게 날치기 당했다!";
    }
    private void Start()
    {
        charactorState.AddState(StateType.secession, 4);
    }

    public override void BeforeFightStart(Actor target)
    {
        int stealAmount = 500;
        StealGold(target, stealAmount);
    }

    public int StealGold(Actor target, int stealAmount)
    {
        int stealGoldAmount = 0;
        if(target.gold > stealAmount)
        {
            target.gold -= stealAmount;
            gold += stealAmount;
            stealGoldAmount = stealAmount;
        }
        else
        {
            gold += target.gold;
            stealGoldAmount = target.gold;
            target.gold = 0;
        }
        charactorState.AddState(StateType.thief, stealGoldAmount);
        return stealGoldAmount;
    }
}
