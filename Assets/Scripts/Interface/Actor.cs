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


    #region 캐릭터 능력치 관련 변수, 함수
    private const int MAX_HP = 100;
    private int _hp = 100;
    public int hp
    {
        get { return _hp; }
        set { _hp = value; }
    }
    public int _protect = 0;
    public int protect
    {
        get { return _protect; }
        set { _protect = value; }
    }


    private int _burnStack = 0;
    public int burnStack
    {
        get { return _burnStack; }
        set { _burnStack = value; }
    }
    private int _weakenStack = 0;
    public int weakenStack
    {
        get { return _weakenStack; }
        set { _weakenStack = value; }
    }
    private int _additionalStack = 0;
    public int additionalStack
    {
        get { return _additionalStack; }
        set { _additionalStack = value; }
    }

    #endregion

    private bool _burnAttack = false;
    public bool burnAttack
    {
        get { return _burnAttack; }
        set { _burnAttack = value; }
    }    
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
        ShowKeywordMain();
    }

    internal void GetKeywordMain(KeywordMain _keywordMain)
    {
        keywordMain = _keywordMain;
        mainKeywords.SetActive(false);
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
            case DamageType.Burn:
                Debug.Log(gameObject.name + "화염 피해" + _damage);
                break;

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
}
