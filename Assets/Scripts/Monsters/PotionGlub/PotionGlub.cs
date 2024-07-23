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

    private int _potionNum = 0;
    private PotionColor _potionColor = 0;
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
    }

    public override void Damaged(Actor attacker, int _damage)
    {
        if (_damage <= 0)
            return;
        int totalDamage = _damage;


        if(attacker != this)
            PotionHitted(attacker);

        if (totalDamage > 0)
        {
            attackCount = true;
        }
        if (additionalDamage > 0)
        {
            totalDamage += additionalDamage;
            additionalDamage = 0;
        }
        if (charactorState.GetStateStack(StateType.weaken) > 0)
        {
            totalDamage += charactorState.GetStateStack(StateType.weaken);
            charactorState.ReductionByValue(StateType.weaken, 1);
        }
        if (attacker.charactorState.GetStateStack(StateType.reduction) > 0)
        {
            if (totalDamage < attacker.charactorState.GetStateStack(StateType.reduction))
            {
                totalDamage = 0;
                attacker.charactorState.ReductionByValue(StateType.reduction, 1);
            }
            else
            {
                totalDamage -= attacker.charactorState.GetStateStack(StateType.reduction);
                attacker.charactorState.ReductionByValue(StateType.reduction, 1);
            }
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
            charactorState.AddAllActiveState(5);
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
