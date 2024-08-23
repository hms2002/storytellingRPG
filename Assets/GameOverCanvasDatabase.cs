using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCanvasDatabase : MonoBehaviour
{
    public static GameOverCanvasDatabase instance;
    public GameObject GameOverCanvas;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
