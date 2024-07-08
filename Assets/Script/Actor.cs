using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Burn,
    Beat
}

public class Actor
{
    private int hp;
    private int protect;

    private int burnStack;
    private int vulnerableStack;

    public void BeforeAction()
    {
        if (burnStack > 0)
        {
            Damaged(burnStack * 2, DamageType.Burn);
            burnStack -= 1;
        }
    }

    public void Burn(int burnRate)
    {
        burnStack += burnRate;
    }

    public void Vulnerable(int vulnerableRate)
    {

    }

    public void AddProtect(int protectRate)
    {

    }

    public void AddHp(int healingRate)
    {

    }

    public void Damaged(int damage, DamageType type)
    {
        switch(type)
        {
            case DamageType.Burn:
                hp -= damage;
                break;
            case DamageType.Beat:
                hp -= damage + vulnerableStack;
                vulnerableStack -= 1;
                break;
        } 
    }
}
