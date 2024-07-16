using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicSlime : Actor
{
    [Header("유독성 슬라임 제원 제어")]
    [SerializeField] private const int _addictionDamage = 5;            // 중독 스택 당 데미지
    [SerializeField] private const int _maxAddictionStack = 8;          // 최대 중독 스택 중첩량
    [SerializeField] private const int _tensionPerAddictionStack = 5;   // 중독 스택 당 긴장도 증가 수치

    #region 유독성 슬라임 제원 변수 프로퍼티
    public int addictionStack
    {
        get { return _addictionStack; }
        set
        {
            _addictionStack = value;
            //stateUIController.AddictionOn(_addictionStack);
        }
    }
    public int addictionDamage { get { return _addictionDamage; } }
    public int maxAddictionStack { get { return _maxAddictionStack; } }
    public int tensionPerAddictionStack { get { return _tensionPerAddictionStack; } }
    #endregion


    /*==================================================================================================================================*/


    private void Start()
    {
        MAX_HP = 70;
        hp = MAX_HP;
    }

    public override void Damaged(Actor attacker, int damage, DamageType type)
    {
        if (damage <= 0) return;

        int totalDamage = damage;

        switch (type)
        {
            case DamageType.Burn:

                Debug.Log(gameObject.name + "화염 피해" + damage);
                break;

            case DamageType.Venom:

                Debug.Log(gameObject.name + "맹독 피해" + damage);
                break;

            case DamageType.Beat:

                Debug.Log(gameObject.name + "타격 피해" + damage);

                if (totalDamage > 0)
                {
                    attackCount = true;
                }
                if (additionalDamage > 0)
                {
                    totalDamage += additionalDamage;
                }
                if (oneTimeReinforce > 0)
                {
                    totalDamage += oneTimeReinforce;
                    oneTimeReinforce = 0;
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
};
