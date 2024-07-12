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
        set { _potionColor = value; }
    }

    public bool isJellyShot
    {
        get { return _isJellyShot; }
        set { _isJellyShot = value; }
    }

    public override void Damaged(Actor attacker, int _damage, DamageType _type)
    {
        if (_damage <= 0)
            return;
        int totalDamage = _damage;
        switch (_type)
        {
            case DamageType.Burn:
                Debug.Log(gameObject.name + "ȭ�� ����" + _damage);
                break;

            case DamageType.Beat:
                Debug.Log(gameObject.name + "Ÿ�� ����" + _damage);
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
        if(potionNum > 1 && potionNum < 4)
        {
            potionColor = PotionColor.Purple;
        }
        if(potionNum == 4)
        {
            potionColor = PotionColor.Black;
        }
        if (potionNum > 5 && potionNum < 8)
        {
            potionColor = PotionColor.Green;
        }
        if (potionNum == 8)
        {
            potionColor = PotionColor.Black;
        }
        if (potionNum > 9 && potionNum < 12)
        {
            potionColor = PotionColor.Purple;
        }
        if (potionNum > 12 && potionNum < 21)
        {
            potionColor = PotionColor.Black;
        }
        if (potionNum > 21)
        {
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
            for (int i = 0; i < buffList.Length; i++)
            {
                if (attacker.buffList[i] > 0)
                {
                    attacker.buffList[i] += 5;
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
