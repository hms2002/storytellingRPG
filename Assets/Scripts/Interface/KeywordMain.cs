using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KeywordMain : MonoBehaviour
{
    protected int damage = 0;
    protected int additionalDamage = 0;
    protected int repeatCount = 1;

    public abstract void Execute(Actor caster, Actor target, Sentence sentence);
    public void AddDamage(int addRate)
    {
        additionalDamage += addRate;
        Debug.Log("추가 공격력 : " + additionalDamage);
    }
    public void UpRepeatCount(int repeatRate)
    {
        repeatCount += repeatRate;
    }
    
}
