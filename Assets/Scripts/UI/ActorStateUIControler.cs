using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ActorStateUIControler : MonoBehaviour
{
    public Transform pivot;
    public Image hpSlider;
    GameObject[] stateUIObjects = new GameObject[(int)StateType.Size];
    public GameObject protectUIObject;
    public GameObject hpUI;
    public TextMeshProUGUI hpText;

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

        TextMeshProUGUI text = stateUIObjects[typeNum].GetComponentInChildren<TextMeshProUGUI>();
        text.text = state.stack + "";
    }

    internal void DestroySelf()
    {
        foreach( GameObject i in stateUIObjects)
        {
            if (i == null) continue;
            Destroy(i);
        }
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
        TextMeshProUGUI text = protectUIObject.GetComponentInChildren<TextMeshProUGUI>();
        text.text = rate + "";
    }

    public void UpdateHpUI(int hp, int MAX_HP)
    {
        hpSlider.fillAmount = hp / (float)MAX_HP;
        hpText.text = hp + " / " + MAX_HP;
    }
}
