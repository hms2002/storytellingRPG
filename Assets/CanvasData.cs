using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasData : MonoBehaviour
{
    [SerializeField] private GameObject _handCanvas;

    private static CanvasData _canvasData;

    public GameObject handCanvas
    {
        get { return _handCanvas; }
    }

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
