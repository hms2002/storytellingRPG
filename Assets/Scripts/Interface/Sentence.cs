using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentence : MonoBehaviour
{
    private int burnStack = 0;
    private int weakenStack = 0;
    private int repeatStack = 0;
    private int damage = 0;
    private int protect = 0;
    private int heal = 0;
    private int sheidDamage = 0;
    private int pike;
    
    public void DamageControl(int _rate)
    {
        damage += _rate;
    }

    public void ProtectControl(int _rate)
    {
        protect += _rate;
    }

    public void BurnControl(int _rate)
    {
        burnStack += _rate;
    }
    public void WeakenControl(int _rate)
    {
        weakenStack += _rate;
    }

    public void ReapeatControl(int _rate)
    {
        repeatStack += _rate;
    }

    public void HealControl(int _rate)
    {
        heal += _rate;
    }

    public void SheidDamageControl(Actor _caster)
    {
        sheidDamage += _caster.GetProtect();
    }

    public void PikeControl(int _rate) //���� �� �ݻ��
    {
        pike += _rate;
    }

    public void execute(Actor caster, Actor target)
    {
        for (int i = 0; i <= repeatStack; i++)
        {   
            target.Burn(burnStack);
            target.Weaken(weakenStack);
            target.Damaged(damage,DamageType.Beat);
            target.Damaged(sheidDamage, DamageType.Beat);
            caster.AddProtect(protect);
            caster.AddHp(heal);
            Debug.Log(target.gameObject.name + " ���� ü�� : " + target.GetHp());

            if (target.AttackCount == true)
            {
                target.Damaged(pike,DamageType.Beat); 
                //?? actor.isAttack�� ���� �ʱ�ȭ ��Ű��
            }

            target.AttackCount = false;
        }
    }
}
