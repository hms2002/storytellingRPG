using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager uIManager;

    private GameObject playerPrefab;

    [Header("UI 캔버스")]
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

    /// <summary>
    /// 전장 탈주 시 UI 세팅 및 책 애니메이션 재생 메소드
    /// </summary>
    private void GetOutOfBattleField()
    {
        // 플레이어 프리팹 
        playerPrefab = GameObject.FindGameObjectWithTag("Player");

        // 플레이어 프리팹 삭제
        Destroy(playerPrefab);

        // 전투 관련 캔버스 끄기
        keywordCanvas.SetActive(false);
        textBoxCanvas.SetActive(false);

    } // 긴장도, 플레이어 체력 등은 전투 돌입 시 초기화하도록 당부함
};
