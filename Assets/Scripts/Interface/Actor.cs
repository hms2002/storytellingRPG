using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Burn,
    Beat
}

public class Actor : MonoBehaviour
{
    private int hp;
    private int protect = 0;

    private int burnStack = 0;
    private int weakenStack = 0;

    public void BeforeAction()
    {
        if (burnStack > 0)
        {
            Damaged(burnStack * 2, DamageType.Burn);
            burnStack -= 1;
        }
    }

    public void Burn(int _burnRate)
    {
        burnStack += _burnRate;
    }

    public void Weaken(int _weakenRate)
    {
        weakenStack += _weakenRate;
    }

    public void AddProtect(int _protectRate)
    {
        protect += _protectRate;
    }

    public void AddHp(int _healingRate)
    {
        hp += _healingRate;
    }

    public void Damaged(int _damage, DamageType _type)
    {
        int totalDamage = _damage;
        switch(_type)
        {
            case DamageType.Burn:

                break;
            case DamageType.Beat:
                if(weakenStack > 0)
                {
                    totalDamage += weakenStack;
                    weakenStack -= 1;
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
            }
        }
        hp -= totalDamage;
    }
}
