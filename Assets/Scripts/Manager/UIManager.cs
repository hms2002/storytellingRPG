using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager uIManager;

    [Header("캔버스")]
    [SerializeField] private GameObject stateCanvas;
    [SerializeField] private GameObject keywordCanvas;
    [SerializeField] private GameObject textBoxCanvas;

    [Header("책")]
    [SerializeField] private Animator bookAnimator;


    private bool _isBattleOver = false;
    public bool isBattleOver { get => isBattleOver; set => isBattleOver = value; }


    /*==================================================================================================================================*/


    private void Awake()
    {
        // 싱글톤 구조 보강
        if (uIManager != null && uIManager != this)
        {
            Destroy(this.gameObject);
            return;
        }
        uIManager = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void GetOutOfBattleField()
    {
        stateCanvas.SetActive(false);
    }
};
