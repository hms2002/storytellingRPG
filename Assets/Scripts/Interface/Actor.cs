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
    private int hp = 100;
    private int protect = 0;

    private int burnStack = 0;
    private int weakenStack = 0;

    public int GetHp()
    {
        return hp;
    }

    public int GetProtect()
    {
        return protect;
    }

    public int GetWeakenStatck()
    {
        return weakenStack;
    }

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
            //화상 데미지
            case DamageType.Burn:

                break;

            //일반 데미지
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
