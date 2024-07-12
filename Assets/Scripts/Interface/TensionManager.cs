using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TensionManager : MonoBehaviour
{
    private int _tension = 100;
    private const int _BASIC_MAX_TENSION = 100;
    public Slider tensionSlider;
    public TextMeshProUGUI tensionText;

    private static TensionManager _tensionManagerUI;

    public static TensionManager tensionManagerUI
    {
        get { return _tensionManagerUI; }
    }

    private void Awake()
    {
        if (_tensionManagerUI != null)
            return;
        _tensionManagerUI = this;
    }
    public int BASIC_MAX_TENSION
    {
        get { return _BASIC_MAX_TENSION; }
    }

    public int tension
    {
        get { return _tension; }
        set
        {
            _tension = value;
            if (_tension > BASIC_MAX_TENSION)
            {
                _tension = BASIC_MAX_TENSION;
            }
            tensionSlider.value = tension / (float)BASIC_MAX_TENSION;
            tensionText.text = tension + " / " + BASIC_MAX_TENSION;
        }
    }
}
