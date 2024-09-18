using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrasureDragon : Monster
{
    public GameObject specialKeywordReward;

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
        gold = 10;
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
    protected int CalculateDragonTreasure(int totalDamage, Actor attacker)
    {
        int dragonTreasure = charactorState.GetStateStack(StateType.treasureOfDragon);
        if(totalDamage >= dragonTreasure)
        {
            totalDamage -= dragonTreasure;
            attacker.gold += dragonTreasure;
            charactorState.ResetState(StateType.treasureOfDragon);
        }
        else
        {
            charactorState.ReductionByValue(StateType.treasureOfDragon, totalDamage);
            attacker.gold += totalDamage;
            totalDamage = 0;
        }    

        return totalDamage;
    }
    protected int CalculateAllProtection(int totalDamage, Actor attacker)
    {
        totalDamage = base.CalculateAllProtection(totalDamage);
        totalDamage = CalculateDragonTreasure(totalDamage, attacker);
        
        return totalDamage;
    }
    public override void Damaged(Actor attacker, DamageInfo _damage)
    {
        if (_damage.damage <= 0) return;

        int totalDamage = _damage.damage;

        if (attacker == this)
            DamagedSelf(totalDamage);
        else
            DamagedOther(totalDamage, attacker);
    }
    public override void DestroySelf()
    {
        RewardManager.instance.AddSpecialReward(specialKeywordReward);
        base.DestroySelf();
    }
    protected override void DamagedOther(int totalDamage, Actor attacker)
    {
        // 공격 전 피해량 계산
        totalDamage = CalculateTotalDamageBeforeDamaged(totalDamage, attacker);

        // 피해량 있으면, 반격 플래그 TRUE
        CheckAttackCountFlag(totalDamage, attacker);

        // 보호막 관련 모든 연산을 실행
        totalDamage = CalculateAllProtection(totalDamage,attacker);
        if (totalDamage <= 0)
        {
            if (dmgList.damageL.Count == 1)
                AudioManager.instance.PlaySound("Character", attackSound);
            totalDamage = 0;
        }
        UIManager.instance.ActiveDamageText(transform.position, totalDamage, Color.red);

        beforeDamage = totalDamage;
        hp -= totalDamage;

        // 공격자, 공격 시 스택 감소할 것들 감소
        attacker.charactorState.ReductionOnAttack();
        // 피해자, 피해 시 스택 감소할 것들 감소
        charactorState.ReductionOnDamaged();
    }
}
