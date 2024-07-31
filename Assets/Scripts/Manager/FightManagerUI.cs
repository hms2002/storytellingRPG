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

    

    private void Awake()
    {
        if (_fightManagerUI != null)
            return;
        _fightManagerUI = this;
    }

}
