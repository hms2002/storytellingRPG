using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum DamageType
{
    Burn,
    Beat
}

public class Actor : MonoBehaviour
{
    public Slider hpSlider;
    public TextMeshProUGUI hpText;

    private const int MAX_HP = 100;
    private int hp = 100;
    private int protect = 0;

    private int burnStack = 0;
    private int weakenStack = 0;
    private int additionalStack = 0;

    public bool BurnAttack = false;
    public bool weakenAttack = false;
    public bool AttackCount = false; //���� Ȯ�ο�

    public GameObject supKeywords;
    public GameObject mainKeywords;

    KeywordSup keywordSup;
    KeywordMain keywordMain;

    public void BeforeAction()
    {
        if (burnStack > 0)
        {
            Damaged(burnStack * 2, DamageType.Burn);
            burnStack -= 1;
        }
    }

    internal void GetKeywordSup(KeywordSup _keywordSup)
    {
        keywordSup = _keywordSup;
        // ���� Ű���� ���� ���� ���� Ű���� ����
        ShowKeywordMain();
    }

    internal void GetKeywordMain(KeywordMain _keywordMain)
    {
        keywordMain = _keywordMain;
        mainKeywords.SetActive(false);
    }

    public void Burn(int _burnRate)
    {
        if(BurnAttack)
        {
            burnStack += _burnRate + additionalStack;
        }
    }

    public void Weaken(int _weakenRate)
    {
        if(weakenAttack)
        {
            weakenStack += _weakenRate + additionalStack;
        }
    }

    public void AdditionalStack(int _additionalStack)
    {
        additionalStack += _additionalStack;
    }

    public void AddProtect(int _protectRate)
    {
        protect += _protectRate;
    }

    public void AddHp(int _healingRate)
    {
        hp += _healingRate;
    }

    internal void Action(Actor target)
    {
        Sentence sentence = new Sentence();

        keywordSup.Check(keywordMain);
        keywordMain.Check(keywordSup);

        protect = 0;

        keywordSup.Execute(this, target, sentence);
        keywordMain.Execute(this, target, sentence);
        sentence.execute(this, target);
        
    }

    public void Damaged(int _damage, DamageType _type)
    {
        int totalDamage = _damage;
        switch(_type)
        {
            //ȭ�� ������
            case DamageType.Burn:
                Debug.Log(gameObject.name + "화염 피해" + _damage);
                break;

            //�Ϲ� ������
            case DamageType.Beat:
                Debug.Log(gameObject.name + "타격 피해" + _damage);
                if (totalDamage > 0)
                {
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
        hp -= totalDamage;

        // 체력 UI 조정
        hpSlider.value = hp/(float)MAX_HP;
        hpText.text = hp + " / " + MAX_HP;
    }

    public void StartTurn()
    {
        // ���� Ű���� ����
        ShowKeywordSup();
    }

    private void ShowKeywordMain()
    {
        supKeywords.SetActive(false);
        mainKeywords.SetActive(true);
    }

    private void ShowKeywordSup()
    {
        supKeywords.SetActive(true);
    }

    public int GetHp() { return hp; }
    public int GetProtect() { return protect; }
    public int GetBurnStack() { return burnStack; }
    public int GetWeakenStack() { return weakenStack; }
}
