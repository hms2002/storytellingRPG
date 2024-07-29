using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 운영에 필요한 UI 관리를 담당
/// <para> 전투 UI, 책 애니메이션 제어 </para>
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager uIManager;

    [Header("전투 기능 및 UI")]
    [SerializeField] private List<GameObject> combatFunctionAndUI;

    [Header("책")]
    [SerializeField] private Animator bookAnimator;

    [Header("아이콘")]
    [SerializeField] private GameObject theEndIcon;

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
    /// 전장에 돌입하면 BookPassR 애니메이션 재생, 전투 UI 활성화하는 메소드
    /// </summary>
    public IEnumerator EnterBattleField()
    {
        bookAnimator.SetTrigger("shouldTurnPageToRight");

        yield return new WaitForSeconds(1.5f);

        ActiveCombatFunctionAndUI(true);
    }

    /// <summary>
    /// 전장에서 벗어나면 Player 제거, 전투 UI 비활성화, BookPassR 애니메이션 재생하는 메소드
    /// </summary>
    public void GetOutOfBattleField()
    {
        // 전투 관련 캔버스 끄기
        ActiveCombatFunctionAndUI(false);

        // 책 페이지 오른쪽으로 넘기도록 트리거 발동
        bookAnimator.SetTrigger("shouldTurnPageToRight");

    } // 긴장도, 플레이어 체력 등은 전투 돌입 시 초기화하도록 당부함

    /// <summary>
    /// 전투 관련 기능 및 UI 활성화 여부를 일괄 관리하는 메소드
    /// </summary>
    /// <param name="enableOrNot">아이콘 SetActive() 여부</param>
    public void ActiveCombatFunctionAndUI(bool enableOrNot)
    {
        for (int i = 0; i < combatFunctionAndUI.Count; i++)
        {
            combatFunctionAndUI[i].SetActive(enableOrNot);
        }
    }

    /// <summary>
    /// The End 아이콘을 활성화 혹은 비활성화하는 메소드
    /// </summary>
    /// <param name="enableOrNot">아이콘 SetActive() 여부</param>
    public void ActiveTheEndIcon(bool enableOrNot)
    {
        theEndIcon.SetActive(enableOrNot);
    }
};
