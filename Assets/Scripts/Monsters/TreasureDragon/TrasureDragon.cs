using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrasureDragon : Actor
{
    private int _dragonTrasure = 200;
    private int _trasureDamage = 0;
    private int _motherDragonsCall = 5;

    public int dragonTrasure
    {
        get { return _dragonTrasure; }
        set { _dragonTrasure = value; }
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
        Sentence sentence = new Sentence();

        keywordSup.Check(keywordMain);
        keywordMain.Check(keywordSup);

        protect = 0;
        motherDragonsCall -= 1;

        keywordSup.Execute(this, target, sentence);
        keywordMain.Execute(this, target, sentence);
        sentence.execute(this, target);
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
        if (dragonTrasure > 0)
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
                if (dragonTrasure < totalDamage)
                {
                    totalDamage -= dragonTrasure;
                    trasureDamage = totalDamage;
                    dragonTrasure = 0;
                }
                else
                {
                    dragonTrasure -= totalDamage;
                    trasureDamage = totalDamage;
                    totalDamage = 0;
                }
            }
        }
        hp -= totalDamage;
    }
}
