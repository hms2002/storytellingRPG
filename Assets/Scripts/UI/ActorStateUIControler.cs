using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActorStateUIControler : MonoBehaviour
{
    public Transform pivot;
    public Image hpSlider;
    public GameObject[] stateUIObjects = new GameObject[(int)StateType.Size];
    public GameObject protectUIObject;
    public GameObject hpUI;
    public Text hpText;

    public void UpdateUI(State state)
    {
        if(state == null)
        {
            Debug.LogError("NULL STATE UI UPDATE");
            return;
        }
        int typeNum = (int)state.stateData.type;
        if (stateUIObjects[typeNum] == null)
        {
            stateUIObjects[typeNum] = Instantiate(StateUIDatabase.stateUIDB.stateUIPrefab, pivot);
            stateUIObjects[typeNum].GetComponent<Image>().sprite
                    = state.stateData.stateImage;
            stateUIObjects[typeNum].GetComponent<ShowStateInfo>().data
                    = state.stateData;
        }

        if (state.stack <= 0)
            stateUIObjects[typeNum].SetActive(false);
        else
            stateUIObjects[typeNum].SetActive(true);

        Text text = stateUIObjects[typeNum].GetComponentInChildren<Text>();
        text.text = state.stack + "";
    }

    public void ProtectOn(int rate)
    {
        if (rate <= 0)
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

    public void UpdateHpUI(int hp, int MAX_HP)
    {
        hpSlider.fillAmount = hp / (float)MAX_HP;
        hpText.text = hp + " / " + MAX_HP;
    }
}

    //}
    //GameObject burnUI;
    //public void BurnOn(int rate)
    //{
    //    if (burnUI == null)
    //    {
    //        burnUI = Instantiate(StateUIDatabase.stateUIDB.burn);
    //        burnUI.transform.SetParent(pivot);
    //    }
    //    if (rate <= 0)
    //    {
    //        burnUI.SetActive(false);
    //        return;
    //    }

    //    burnUI.SetActive(true);
    //    Text text = burnUI.GetComponentInChildren<Text>();
    //    text.text = rate + "";
    //}
    //GameObject weakUI;
    //public void WeakenOn(int rate)
    //{
    //    if (weakUI == null)
    //    {
    //        weakUI = Instantiate(StateUIDatabase.stateUIDB.weaken);
    //        weakUI.transform.SetParent(pivot);
    //    }
    //    if (rate <= 0)
    //    {
    //        weakUI.SetActive(false);
    //        return;
    //    }

    //    weakUI.SetActive(true);
    //    Text text = weakUI.GetComponentInChildren<Text>();
    //    text.text = rate + "";
    //}
    //GameObject RuctionUI;
    //public void RuctionOn(int rate)
    //{
    //    if (RuctionUI == null)
    //    {
    //        RuctionUI = Instantiate(StateUIDatabase.stateUIDB.Ruction);
    //        RuctionUI.transform.SetParent(pivot);
    //    }
    //    if (rate <= 0)
    //    {
    //        RuctionUI.SetActive(false);
    //        return;
    //    }

    //    RuctionUI.SetActive(true);
    //    Text text = RuctionUI.GetComponentInChildren<Text>();
    //    text.text = rate + "";
    //}
    //GameObject glassFragmentUI;
    //public void GlassFragmentOn(int rate)
    //{
    //    if (glassFragmentUI == null)
    //    {
    //        glassFragmentUI = Instantiate(StateUIDatabase.stateUIDB.glassFragment);
    //        glassFragmentUI.transform.SetParent(pivot);
    //    }
    //    if (rate <= 0)
    //    {
    //        glassFragmentUI.SetActive(false);
    //        return;
    //    }

    //    glassFragmentUI.SetActive(true);
    //    Text text = glassFragmentUI.GetComponentInChildren<Text>();
    //    text.text = rate + "";
    //}

    //GameObject treasureOfDragonUI;
    //public void TreasureOfDragonOn(int rate)
    //{
    //    if (treasureOfDragonUI == null) 
    //    {
    //        treasureOfDragonUI = Instantiate(StateUIDatabase.stateUIDB.treasureOfDragon);
    //        treasureOfDragonUI.transform.SetParent(pivot);
    //    }
    //    if (rate <= 0)
    //    {
    //        treasureOfDragonUI.SetActive(false);
    //        return;
    //    }

    //    treasureOfDragonUI.SetActive(true);
    //    Text text = treasureOfDragonUI.GetComponentInChildren<Text>();
    //    text.text = rate + "";
    //}
    //GameObject callingOfMommyUI;
    //public void CallingOfMommyOn(int rate)
    //{
    //    if (callingOfMommyUI == null)
    //    {
    //        callingOfMommyUI = Instantiate(StateUIDatabase.stateUIDB.callingOfMommy);
    //        callingOfMommyUI.transform.SetParent(pivot);
    //    }
    //    if (rate <= 0)
    //    {
    //        callingOfMommyUI.SetActive(false);
    //        return;
    //    }

    //    callingOfMommyUI.SetActive(true);
    //    Text text = callingOfMommyUI.GetComponentInChildren<Text>();
    //    text.text = rate + "";
    //}

    //GameObject powerUI;
    //public void PowerOn(int rate)
    //{
    //    if (powerUI == null)
    //    {
    //        powerUI = Instantiate(StateUIDatabase.stateUIDB.power);
    //        powerUI.transform.SetParent(pivot);
    //    }
    //    if (rate <= 0)
    //    {
    //        powerUI.SetActive(false);
    //        return;
    //    }

    //    powerUI.SetActive(true);
    //    Text text = powerUI.GetComponentInChildren<Text>();
    //    text.text = rate + "";
    //}