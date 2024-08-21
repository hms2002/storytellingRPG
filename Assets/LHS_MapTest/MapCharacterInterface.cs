using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MapCharacterInterface : MonoBehaviour
{
    [Header("가나다")]
    public Actor playerAction;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI tensionText;

    private bool loding = true;

    private void Update()
    {
        StateUpdate();
    }

    private void StateUpdate()
    {
        moneyText.text = "" + playerAction.gold + "G"; //나중에 작성
        if (loding)
        {
            hpText.text = playerAction.hp + "/" + playerAction.MAX_HP;
        }
        else
        {
            hpText.text = playerAction.hp + "/" + playerAction.MAX_HP;
        }

        tensionText.text = TensionManager.tensionManagerUI.tension + "/" + 100;
    }

}