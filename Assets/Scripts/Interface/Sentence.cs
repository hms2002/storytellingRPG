using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentence : MonoBehaviour
{
    private int burnStack = 0;
    private int selfBurnStack = 0;
    private int weakenStack = 0;
    private int selfWeakenStack = 0;
    private int repeatStack = 1;
    private int damage = 0;
    private int protect = 0;
    private int heal = 0;
    private int sheidDamage = 0;
    private int pike = 0;
    private int additionalStack = 0;
    
    
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

    public void SetXRepeat(int _times)
    {
        repeatStack *= _times;
    }

    public void HealControl(int _rate)
    {
        heal += _rate;
    }

    public void SheidDamageControl(Actor _caster)
    {
        sheidDamage += _caster.protect;
    }

    public void PikeControl(int _rate) //���� �� �ݻ��
    {
        pike += _rate;
    }

    public void execute(Actor caster, Actor target)
    {
        for (int i = 1; i <= repeatStack; i++)
        {
            target.burnStack += burnStack;
            target.weakenStack += weakenStack;
            target.Damaged(damage,DamageType.Beat);
            target.Damaged(sheidDamage, DamageType.Beat);
            caster.protect += (protect);
            caster.hp += heal;
            caster.weakenStack += selfWeakenStack;
            caster.burnStack += selfBurnStack;
            Debug.Log(target.gameObject.name + " 체력 : " + target.hp);

            Debug.Log("입히는 데미지 : " + damage);
            Debug.Log("상태 방어력 : " + caster.protect);
            Debug.Log(target.gameObject.name + "화염 스택 : " + target.burnStack);
            if (target.AttackCount == true)
            {
                caster.Damaged(pike,DamageType.Beat);
                //?? actor.isAttack�� ���� �ʱ�ȭ ��Ű��
            }

            target.AttackCount = false;
            target.weakenAttack = false;
            target.burnAttack = false;
        }
    }
}
