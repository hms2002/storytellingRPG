using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Burn,
    Beat
}

public class Actor : MonoBehaviour
{
    private int hp = 100;
    private int protect = 0;

    private int burnStack = 0;
    private int weakenStack = 0;

    public bool AttackCount = false; //가시 확인용

    public GameObject supKeywords;
    public GameObject mainKeywords;

    KeywordSup keywordSup;
    KeywordMain keywordMain;

    public int GetHp()
    {
        return hp;
    }

    public int GetProtect()
    {
        return protect;
    }

    internal void GetKeywordSup(KeywordSup _keywordSup)
    {
        keywordSup = _keywordSup;
        // 보조 키워드 선택 이후 메인 키워드 선택
        ShowKeywordMain();
    }

    public void BeforeAction()
    {
        if (burnStack > 0)
        {
            Damaged(burnStack * 2, DamageType.Burn);
            burnStack -= 1;
        }
    }

    internal void GetKeywordMain(KeywordMain _keywordMain)
    {
        keywordMain = _keywordMain;
        mainKeywords.SetActive(false);
    }

    public void Burn(int _burnRate)
    {
        burnStack += _burnRate;
    }

    public void Weaken(int _weakenRate)
    {
        weakenStack += _weakenRate;
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
        keywordSup.Execute(this, target, sentence);
        keywordMain.Execute(this, target, sentence);
        sentence.execute(this, target);
    }

    public void Damaged(int _damage, DamageType _type)
    {
        int totalDamage = _damage;
        switch(_type)
        {
            //화상 데미지
            case DamageType.Burn:

                break;

            //일반 데미지
            case DamageType.Beat:
                if(weakenStack > 0)
                {
                    totalDamage += weakenStack;
                    weakenStack -= 1;
                    AttackCount = true;
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
            }
        }
        hp -= totalDamage;
    }

    public void StartTurn()
    {
        // 보조 키워드 선택
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
