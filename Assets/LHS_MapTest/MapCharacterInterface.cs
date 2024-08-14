using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapCharacterInterface : MonoBehaviour
{
    [Header("가나다")]
    public Actor playerAction;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI tensionText;

    private void OnEnable()
    {
        StateUpdate();
    }

    private void StateUpdate()
    {
        moneyText.text = "" + playerAction.gold; //나중에 작성
        hpText.text = "" + playerAction.hp;
        tensionText.text = "" + playerAction.tension;
    }

}