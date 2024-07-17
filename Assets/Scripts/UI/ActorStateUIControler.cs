using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActorStateUIControler : MonoBehaviour
{

    enum STATE_UI_INDEX
    {
        PROTECT = 0,        // 보호막
        BURN,               // 화상
        WEAKEN,             // 맞을 때 더 아프게 맞음
        REDUCTION,          // 때릴 때 더 약해하게 때림
        GLASS_FRAGMENT,     // 맞으면 스택만큼 반사
        TREASURE_OF_DRAGON, // 용의 보물
        CALLING_OF_MOMMY,   // 어미 용의 부름
        END_INDEX
    }

    public Image hpSlider;
    [Header("방어, 화염, 취약, 약화, 유리조각, 용의 보물, 어미 용의 부름 순서로 넣기")] 
    public List<GameObject> stateUIObjects;
    public GameObject hpUI;
    public Text hpText;


    public void ProtectOn(int rate)
    {
        if(rate <= 0)
        {
            hpUI.SetActive(true);
            stateUIObjects[(int)STATE_UI_INDEX.PROTECT].SetActive(false);
            return;
        }

        hpUI.SetActive(false);
        stateUIObjects[(int)STATE_UI_INDEX.PROTECT].SetActive(true);
        Text text = stateUIObjects[(int)STATE_UI_INDEX.PROTECT].GetComponentInChildren<Text>();
        text.text = rate + "";
    }
    public void BurnOn(int rate)
    {
        if (rate <= 0)
        {
            stateUIObjects[(int)STATE_UI_INDEX.BURN].SetActive(false);
            return;
        }

        stateUIObjects[(int)STATE_UI_INDEX.BURN].SetActive(true);
        Text text = stateUIObjects[(int)STATE_UI_INDEX.BURN].GetComponentInChildren<Text>();
        text.text = rate + "";
    }
    public void WeakenOn(int rate)
    {
        if (rate <= 0)
        {
            stateUIObjects[(int)STATE_UI_INDEX.WEAKEN].SetActive(false);
            return;
        }

        stateUIObjects[(int)STATE_UI_INDEX.WEAKEN].SetActive(true);
        Text text = stateUIObjects[(int)STATE_UI_INDEX.WEAKEN].GetComponentInChildren<Text>();
        text.text = rate + "";
    }
    public void ReductionOn(int rate)
    {
        if (rate <= 0)
        {
            stateUIObjects[(int)STATE_UI_INDEX.REDUCTION].SetActive(false);
            return;
        }

        stateUIObjects[(int)STATE_UI_INDEX.REDUCTION].SetActive(true);
        Text text = stateUIObjects[(int)STATE_UI_INDEX.REDUCTION].GetComponentInChildren<Text>();
        text.text = rate + "";
    }

    public void GlassFragmentOn(int rate)
    {
        if (rate <= 0)
        {
            stateUIObjects[(int)STATE_UI_INDEX.GLASS_FRAGMENT].SetActive(false);
            return;
        }

        stateUIObjects[(int)STATE_UI_INDEX.GLASS_FRAGMENT].SetActive(true);
        Text text = stateUIObjects[(int)STATE_UI_INDEX.GLASS_FRAGMENT].GetComponentInChildren<Text>();
        text.text = rate + "";
    }

    public void TreasureOfDragonOn(int rate)
    {
        if (rate <= 0)
        {
            stateUIObjects[(int)STATE_UI_INDEX.TREASURE_OF_DRAGON].SetActive(false);
            return;
        }

        stateUIObjects[(int)STATE_UI_INDEX.TREASURE_OF_DRAGON].SetActive(true);
        Text text = stateUIObjects[(int)STATE_UI_INDEX.TREASURE_OF_DRAGON].GetComponentInChildren<Text>();
        text.text = rate + "";
    }
    public void CallingOfMommyOn(int rate)
    {
        if (rate <= 0)
        {
            stateUIObjects[(int)STATE_UI_INDEX.CALLING_OF_MOMMY].SetActive(false);
            return;
        }

        stateUIObjects[(int)STATE_UI_INDEX.CALLING_OF_MOMMY].SetActive(true);
        Text text = stateUIObjects[(int)STATE_UI_INDEX.CALLING_OF_MOMMY].GetComponentInChildren<Text>();
        text.text = rate + "";
    }
    public void UpdateHpUI(int hp, int MAX_HP)
    {
        hpSlider.fillAmount = hp / (float)MAX_HP;
        hpText.text = hp + " / " + MAX_HP;
    }
}
