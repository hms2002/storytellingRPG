using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasData : MonoBehaviour
{
    public GameObject mainHand;
    public GameObject supHand;

    private static CanvasData _canvasData;

    public static CanvasData canvasData
    {
        get { return _canvasData; }
    }
    private void Awake()
    {
        if (_canvasData != null)
            return;
        _canvasData = this;
    }
}
