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
    }
    protected int CalculateDragonTreasure(int totalDamage)
    {
        int dragonTreasure = charactorState.GetStateStack(StateType.treasureOfDragon);
        if(totalDamage >= dragonTreasure)
        {
            totalDamage -= dragonTreasure;
            charactorState.ResetState(StateType.treasureOfDragon);
        }
        else
        {
            charactorState.ReductionByValue(StateType.treasureOfDragon, totalDamage);
            // 플레이어 돈 주는 코드 넣어야 함.
            totalDamage = 0;
        }    

        return totalDamage;
    }
    protected override int CalculateAllProtection(int totalDamage)
    {
        totalDamage = base.CalculateAllProtection(totalDamage);
        totalDamage = CalculateDragonTreasure(totalDamage);
        
        return totalDamage;
    }
    public override void Damaged(Actor attacker, int _damage)
    {
        if (_damage <= 0) return;

        int totalDamage = _damage;

        if (attacker == this)
            DamagedSelf(totalDamage);
        else
            DamagedOther(totalDamage, attacker);
    }
}
