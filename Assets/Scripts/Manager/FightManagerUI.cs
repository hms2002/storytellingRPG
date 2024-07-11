using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FightManagerUI : MonoBehaviour
{
    private static FightManagerUI _fightManagerUI;
    static public FightManagerUI fightManagerUI
    {
        get { return _fightManagerUI; }
    }

    public TextMeshProUGUI turnText;

    private void Awake()
    {
        if (_fightManagerUI != null)
            return;
        _fightManagerUI = this;
    }

    public void ChangeTurnText(string name)
    {
        TMP_Text tMP_Text = turnText;
        tMP_Text.text = name + "의 키워드";
    }
}
