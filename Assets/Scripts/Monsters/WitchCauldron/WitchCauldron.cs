using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchCauldron : Monster
{
    public enum PotionColor
    {
        Purple = 1,
        Green,
        Red,
        Black,
        Mix,
        Blue
    }

    private int _potionNum = 1;
    private PotionColor _potionColor = PotionColor.Purple;
    private bool _isSpellUp = false;
    private Animator animator;

    public int potionNum
    {
        get { return _potionNum; }
        set
        {
            if (!_isSpellUp)
            {
                _potionNum = value;
                if (_potionNum < 1) _potionNum = 1;
            }
        }
    }

    public PotionColor potionColor
    {
        get { return _potionColor; }
        set { _potionColor = value; }
    }

    public bool isSpellUp
    {
        get { return _isSpellUp; }
        set { _isSpellUp = value; }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        MAX_HP = 64;
        encounterText = "부글부글, 다양한 재료의 냄새가 공간을 가득 채운다.";
    }


    protected override void DamagedOther(int totalDamage, Actor attacker)
    {
        // 공격 전 피해량 계산
        totalDamage = CalculateTotalDamageBeforeDamaged(totalDamage, attacker);

        // 피해량 있으면, 반격 플래그 TRUE
        CheckAttackCountFlag(totalDamage, attacker);

        // 피해량 있으면 포션글럽 패시브 발동
        if (totalDamage > 0)
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
        if (potionNum >= 1 && potionNum < 4)
        {
            animator.SetTrigger("isPurple");
            potionColor = PotionColor.Purple;
        }
        if (potionNum >= 4 && potionNum < 7)
        {
            animator.SetTrigger("isGreen");
            potionColor = PotionColor.Green;
        }
        if (potionNum >= 7 && potionNum < 10)
        {
            animator.SetTrigger("isRed");
            potionColor = PotionColor.Red;
        }
        if (potionNum >= 11 && potionNum < 14)
        {
            animator.SetTrigger("isBlack");
            potionColor = PotionColor.Black;
        }
        if (potionNum >= 14 && potionNum < 20)
        {
            animator.SetTrigger("isMix");
            potionColor = PotionColor.Mix;
        }
        if (potionNum >= 21)
        {
            animator.SetTrigger("isBlue");
            potionColor = PotionColor.Blue;
        }
    }

    public void PotionHitted(Actor attacker)
    {
        if (potionColor == PotionColor.Purple)
        {
            attacker.charactorState.AddState(StateDatabase.stateDatabase.
            venom, 3);
        }
        if (potionColor == PotionColor.Green)
        {
            attacker.charactorState.AddState(StateDatabase.stateDatabase.
                weaken, 3);
        }
        if (potionColor == PotionColor.Red)
        {
            attacker.charactorState.AddState(StateDatabase.stateDatabase.
                burn, 3);
        }
        if (potionColor == PotionColor.Black)
        {
            attacker.charactorState.AddAllActiveState(5);
        }
        if (potionColor == PotionColor.Mix)
        {
            attacker.charactorState.AddState(StateDatabase.stateDatabase.
            venom, 3);
            attacker.charactorState.AddState(StateDatabase.stateDatabase.
                weaken, 3);
            attacker.charactorState.AddState(StateDatabase.stateDatabase.
                burn, 3);
        }
        if (potionColor == PotionColor.Blue)
        {
            TensionManager tensionManager;
            tensionManager = TensionManager.tensionManagerUI;
            tensionManager.tension = tensionManager.BASIC_MAX_TENSION;
            attacker.hp = attacker.MAX_HP;
        }
    }
}

