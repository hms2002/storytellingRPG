using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassSpider : Actor
{
    
    private int _glassFragmentStack = 0;

    public int glassFragmentStack
    {
        get { return _glassFragmentStack; }
        set { _glassFragmentStack = value;
            stateUIController.GlassFragmentOn(_glassFragmentStack);
        }
    }

    private void Awake()
    {
        hp = 50;
        _MAX_HP = 50;
    }

    public override void Damaged(int _damage, DamageType _type)
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
                    glassFragmentStack += 1;
                    AttackCount = true;
                }
                if (weakenStack > 0)
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
                totalDamage = 0;
            }
        }

        pike = glassFragmentStack;
        hp -= totalDamage;

        // 체력 UI 조정
        hpSlider.value = hp / (float)MAX_HP;
        hpText.text = hp + " / " + MAX_HP;
    }
}
