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
    public Transform pivot;
    public Image hpSlider;
    public GameObject protectUIObject;
    public GameObject hpUI;
    public Text hpText;

    public void ProtectOn(int rate)
    {
        if(rate <= 0)
        {
            protectUIObject.SetActive(false);
            hpUI.SetActive(true);

            return;
        }

        hpUI.SetActive(false);
        protectUIObject.SetActive(true);
        Text text = protectUIObject.GetComponentInChildren<Text>();
        text.text = rate + "";
    }
    GameObject burnUI;
    public void BurnOn(int rate)
    {
        if (burnUI == null)
        {
            burnUI = Instantiate(StateUIDatabase.stateUIDB.burn);
            burnUI.transform.SetParent(pivot);
        }
        if (rate <= 0)
        {
            burnUI.SetActive(false);
            return;
        }

        burnUI.SetActive(true);
        Text text = burnUI.GetComponentInChildren<Text>();
        text.text = rate + "";
    }
    GameObject weakUI;
    public void WeakenOn(int rate)
    {
        if (weakUI == null)
        {
            weakUI = Instantiate(StateUIDatabase.stateUIDB.weaken);
            weakUI.transform.SetParent(pivot);
        }
        if (rate <= 0)
        {
            weakUI.SetActive(false);
            return;
        }

        weakUI.SetActive(true);
        Text text = weakUI.GetComponentInChildren<Text>();
        text.text = rate + "";
    }
    GameObject reductionUI;
    public void ReductionOn(int rate)
    {
        if (reductionUI == null)
        {
            reductionUI = Instantiate(StateUIDatabase.stateUIDB.reduction);
            reductionUI.transform.SetParent(pivot);
        }
        if (rate <= 0)
        {
            reductionUI.SetActive(false);
            return;
        }

        reductionUI.SetActive(true);
        Text text = reductionUI.GetComponentInChildren<Text>();
        text.text = rate + "";
    }
    GameObject glassFragmentUI;
    public void GlassFragmentOn(int rate)
    {
        if (glassFragmentUI == null)
        {
            glassFragmentUI = Instantiate(StateUIDatabase.stateUIDB.glassFragment);
            glassFragmentUI.transform.SetParent(pivot);
        }
        if (rate <= 0)
        {
            glassFragmentUI.SetActive(false);
            return;
        }

        glassFragmentUI.SetActive(true);
        Text text = glassFragmentUI.GetComponentInChildren<Text>();
        text.text = rate + "";
    }

    GameObject treasureOfDragonUI;
    public void TreasureOfDragonOn(int rate)
    {
        if (treasureOfDragonUI == null) 
        {
            treasureOfDragonUI = Instantiate(StateUIDatabase.stateUIDB.treasureOfDragon);
            treasureOfDragonUI.transform.SetParent(pivot);
        }
        if (rate <= 0)
        {
            treasureOfDragonUI.SetActive(false);
            return;
        }

        treasureOfDragonUI.SetActive(true);
        Text text = treasureOfDragonUI.GetComponentInChildren<Text>();
        text.text = rate + "";
    }
    GameObject callingOfMommyUI;
    public void CallingOfMommyOn(int rate)
    {
        if (callingOfMommyUI == null)
        {
            callingOfMommyUI = Instantiate(StateUIDatabase.stateUIDB.callingOfMommy);
            callingOfMommyUI.transform.SetParent(pivot);
        }
        if (rate <= 0)
        {
            callingOfMommyUI.SetActive(false);
            return;
        }

        callingOfMommyUI.SetActive(true);
        Text text = callingOfMommyUI.GetComponentInChildren<Text>();
        text.text = rate + "";
    }

    GameObject powerUI;
    public void PowerOn(int rate)
    {
        if (powerUI == null)
        {
            powerUI = Instantiate(StateUIDatabase.stateUIDB.power);
            powerUI.transform.SetParent(pivot);
        }
        if (rate <= 0)
        {
            powerUI.SetActive(false);
            return;
        }

        powerUI.SetActive(true);
        Text text = powerUI.GetComponentInChildren<Text>();
        text.text = rate + "";
    }
    public void UpdateHpUI(int hp, int MAX_HP)
    {
        hpSlider.fillAmount = hp / (float)MAX_HP;
        hpText.text = hp + " / " + MAX_HP;
    }
}
