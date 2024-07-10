using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentence : MonoBehaviour
{
    #region 프로퍼티로 보호되는 변수들
    private int _burnStack = 0;
    private int _selfBurnStack = 0;
    private int _weakenStack = 0;
    private int _selfWeakenStack = 0;
    private int _repeatStack = 1;
    private int _damage = 0;
    private int _protect = 0;
    private int _heal = 0;
    private int _sheidDamage = 0;
    private int _pike = 0;
    private int _additionalStack = 0;
    #endregion
    
    
    public void DamageControl(int _rate)
    {
        damage += _rate;
    }

    #region 프로퍼티
    public int burnStack
    {
        get { return _burnStack; }
        set { _burnStack = value; }
    }
    public int selfBurnStack
    {
        get { return _selfBurnStack; }
        set { _selfBurnStack = value; }
    }
    public int weakenStack
    {
        get { return _weakenStack; }
        set { _weakenStack = value; }
    }
    public int selfWeakenStack
    {
        get { return _selfWeakenStack; }
        set { _selfWeakenStack = value; }
    }
    public int repeatStack
    {
        get { return _repeatStack; }
        set { _repeatStack = value; }
    }
    public int damage
    {
        get { return _damage; }
        set { _damage = value; }
    }
    public int protect
    {
        get { return _protect; }
        set { _protect = value; }
    }
    public int heal
    {
        get { return _heal; }
        set { _heal = value; }
    }
    public int sheidDamage
    {
        get { return _sheidDamage; }
        set { _sheidDamage = value; }
    }
    public int pike
    {
        get { return _pike; }
        set { _pike = value; }
    }
    public int additionalStack
    {
        get { return _additionalStack; }
        set { _additionalStack = value; }
    }
    #endregion

    public void execute(Actor caster, Actor target)
    {
        for (int i = 1; i <= repeatStack; i++)
        {
            target.burnStack += burnStack;
            target.weakenStack += weakenStack;
            target.reductionStack += reductionStack;
            target.Damaged(damage,DamageType.Beat);
            target.Damaged(sheidDamage, DamageType.Beat);
            caster.protect += (protect);
            caster.hp += heal;
            caster.weakenStack += selfWeakenStack;
            caster.burnStack += selfBurnStack;
            target.reductionStack += selfReductionStack;


            #region 로그
            Debug.Log(target.gameObject.name + " 체력 : " + target.hp);
            Debug.Log("입히는 데미지 : " + damage);
            Debug.Log("상태 방어력 : " + caster.protect);
            Debug.Log(target.gameObject.name + "화염 스택 : " + target.burnStack);
            #endregion

            if (target.AttackCount == true)
            {
                caster.Damaged(pike,DamageType.Beat);
                //?? actor.isAttack�� ���� �ʱ�ȭ ��Ű��
            }

            target.AttackCount = false;
        }
    }
}
