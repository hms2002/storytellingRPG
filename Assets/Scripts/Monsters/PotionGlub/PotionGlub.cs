using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionGlub : Monster
{
    public enum PotionColor
    {
        Purple = 1,
        Black,
        Green,
        Red
    }

    private int _potionNum = 1;
    private PotionColor _potionColor = PotionColor.Purple;
    private bool _isJellyShot = false;
    private Animator animator;

    public int potionNum
    {
        get { return _potionNum; }
        set 
        { 
            if(!isJellyShot)
            {
                _potionNum = value;
                if (_potionNum < 1) _potionNum = 1;
            }
        }
    }

    public PotionColor potionColor
    {
        get { return _potionColor; }
        set { _potionColor = PotionColor.Black; }
    }

    public bool isJellyShot
    {
        get { return _isJellyShot; }
        set { _isJellyShot = value; }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        MAX_HP = 40; 
        encounterText = "포션은 마셔도 죽는다. 마시지 않아도 죽는다.";
    }


    protected override void DamagedOther(int totalDamage, Actor attacker)
    {
        // 공격 전 피해량 계산
        totalDamage = CalculateTotalDamageBeforeDamaged(totalDamage, attacker);

        // 피해량 있으면, 반격 플래그 TRUE
        CheckAttackCountFlag(totalDamage, attacker);
    
        // 피해량 있으면 포션글럽 패시브 발동
        if(totalDamage > 0)
            PotionHitted(attacker);

        // 보호막 관련 모든 연산을 실행
        totalDamage = CalculateAllProtection(totalDamage);

        hp -= totalDamage;


        // 공격자, 공격 시 스택 감소할 것들 감소
        attacker.charactorState.ReductionOnAttack();
        // 피해자, 피해 시 스택 감소할 것들 감소
        charactorState.ReductionOnDamaged();
    }

    public void ColorChecking()
    {
        if(potionNum >= 1 && potionNum < 4)
        {
            animator.SetTrigger("isPurple");
            potionColor = PotionColor.Purple;
        }
        if(potionNum == 4)
        {
            animator.SetTrigger("isBlack");
            potionColor = PotionColor.Black;
        }
        if (potionNum >= 5 && potionNum < 8)
        {
            animator.SetTrigger("isGreen");
            potionColor = PotionColor.Green;
        }
        if (potionNum == 8)
        {
            animator.SetTrigger("isBlack");
            potionColor = PotionColor.Black;
        }
        if (potionNum >= 9 && potionNum < 12)
        {
            animator.SetTrigger("isPurple");
            potionColor = PotionColor.Purple;
        }
        if (potionNum >= 12 && potionNum < 21)
        {
            animator.SetTrigger("isBlack");
            potionColor = PotionColor.Black;
        }
        if (potionNum >= 21)
        {
            animator.SetTrigger("isRed");
            potionColor = PotionColor.Red;
        }
    }

    public void PotionHitted(Actor attacker)
    {
        if (potionColor == PotionColor.Purple)
        {
            attacker.charactorState.AddState(StateDatabase.stateDatabase.
            venom, 3);
        }
        if (potionColor == PotionColor.Black)
        {
            attacker.charactorState.AddAllActiveState(5);
        }
        if (potionColor == PotionColor.Green)
        {
            attacker.charactorState.AddState(StateDatabase.stateDatabase.
                weaken, 3);
        }
        if (potionColor == PotionColor.Red)
        {
            TensionManager tensionManager;
            tensionManager = TensionManager.tensionManagerUI;
            tensionManager.tension = tensionManager.BASIC_MAX_TENSION;
            attacker.hp = attacker.MAX_HP;
        }
    }
}
