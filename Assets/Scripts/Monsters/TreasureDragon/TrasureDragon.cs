using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrasureDragon : Monster
{
    private int _trasureDamage = 0;


    public int trasureDamage
    {
        get { return _trasureDamage; }
        set { _trasureDamage = value; }
    }
    private void Awake()
    {
        encounterText = "\"크아아아\" 보물을 지키는 레드 드래곤이 울부짖었다.";
    }
    private void Start()
    {
        charactorState.AddState(StateDatabase.stateDatabase.treasureOfDragon, 200);
        charactorState.AddState(StateDatabase.stateDatabase.callingOfMommyDragon, 5);

    }

    public override void Action(Actor target)
    {
        keywordSup.Check(keywordMain);
        keywordMain.Check(keywordSup);

        charactorState.ReductionOnMyTurn();

        keywordSup.Execute(this, target);
        keywordMain.Execute(this, target);
        Execute(target);
        additionalDamage += charactorState.GetStateStack(StateType.nextTurnDamage);
    }

    public override void Damaged(Actor attacker, int _damage)
    {
        if (_damage <= 0)
            return;
        int totalDamage = _damage;

        totalDamage += attacker.additionalDamage
                + attacker.charactorState.GetStateStack(StateType.oneTimeReinforce)
                + charactorState.GetStateStack(StateType.weaken)
                - charactorState.GetStateStack(StateType.reduction);


        int dragonTreasureStack = charactorState.GetStateStack(StateType.treasureOfDragon);
        if (dragonTreasureStack > 0)
        {
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

            if (dragonTreasureStack < totalDamage)
            {
                totalDamage -= dragonTreasureStack;
                trasureDamage = totalDamage;
                charactorState.ResetState(StateType.treasureOfDragon);
            }
            else
            {
                charactorState.ReductionByValue(StateType.treasureOfDragon, totalDamage);
                trasureDamage = totalDamage;
                totalDamage = 0;
            }
        }
        hp -= totalDamage;
        attacker.charactorState.ReductionOnAttack();
        charactorState.ReductionOnDamaged();
    }
}
