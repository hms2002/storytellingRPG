using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrasureDragon : Actor
{
    private int _dragonsTrasure = 200;
    private int _trasureDamage = 0;
    private int _motherDragonsCall = 5;

    public int dragonsTrasure
    {
        get { return _dragonsTrasure; }
        set { _dragonsTrasure = value; }
    }

    public int trasureDamage
    {
        get { return _trasureDamage; }
        set { _trasureDamage = value; }
    }

    public int motherDragonsCall
    {
        get { return _motherDragonsCall; }
        set { _motherDragonsCall = value; }
    }

    public override void Action(Actor target)
    {
        keywordSup.Check(keywordMain);
        keywordMain.Check(keywordSup);

        protect = 0;
        motherDragonsCall -= 1;

        keywordSup.Execute(this, target);
        keywordMain.Execute(this, target);
        Execute(target);
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
                if (totalDamage > 0)
                {
                    attackCount = true;
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
        if (dragonsTrasure > 0)
        {
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
            else
            {
                if (dragonsTrasure < totalDamage)
                {
                    totalDamage -= dragonsTrasure;
                    trasureDamage = totalDamage;
                    dragonsTrasure = 0;
                }
                else
                {
                    dragonsTrasure -= totalDamage;
                    trasureDamage = totalDamage;
                    totalDamage = 0;
                }
            }
        }
        hp -= totalDamage;
    }
}
