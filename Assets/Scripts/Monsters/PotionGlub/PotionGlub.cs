using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionGlub : Actor
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

    public override void Damaged(Actor attacker, int _damage, DamageType _type)
    {
        if (_damage <= 0)
            return;
        int totalDamage = _damage;
        switch (_type)
        {
            case DamageType.Burn:
                Debug.Log(gameObject.name + "화염 피해" + _damage);
                break;

            case DamageType.Beat:
                Debug.Log(gameObject.name + "타격 피해" + _damage);
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
                if (weakenStack > 0)
                {
                    totalDamage += weakenStack;
                    weakenStack -= 1;
                }
                if (reductionStack > 0)
                {
                    if (totalDamage < reductionStack)
                    {
                        totalDamage = 0;
                        reductionStack -= 1;
                    }
                    else
                    {
                        totalDamage -= reductionStack;
                        reductionStack -= 1;
                    }
                }
                break;
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
            attacker.venomStack += 3;
        }
        if (potionColor == PotionColor.Black)
        {
            for (int i = 0; i < allStateList.Length; i++)
            {
                if (attacker.allStateList[i] == 0)
                {
                    attacker.allStateList[i] += 5;
                }
            }
        }
        if (potionColor == PotionColor.Green)
        {
            attacker.weakenStack += 3;
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
