using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 게임 운영에 필요한 UI 관리를 담당 | 
/// <para> 맵 UI, 덱 세팅 UI, 전투 UI, 책 애니메이션 제어 </para>
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager uIManager;

    [Header("맵")]
    [SerializeField] private List<GameObject> mapUI;        // 맵 관련 모든 UI를 담는 리스트

    [Header("책갈피")]
    [SerializeField] private List<GameObject> bookmarks;    // 북마크 버튼들
    [SerializeField] private GameObject keywordSettingUI;   // 키워드 세팅 UI

    [Header("전투 기능 및 UI")]
    [SerializeField] private List<GameObject> combatFunctionAndUI;      // 전투 관련 모든 UI를 담는 리스트
    [SerializeField] private Deck originalDeck;             // 플레이어가 갖고 있는 오리지널 덱

    [Header("책")]
    [SerializeField] private Animator bookAnimator;         // 책 애니메이터

    [Header("아이콘")]
    [SerializeField] private GameObject theEndIcon;         //게임 오버 아이콘

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
    /// 맵으로 UI 전환하는 메소드
    /// </summary>
    public void EnterMap()
    {

    }

    /// <summary>
    /// 메인 화면에서 키워드 세팅으로 UI 전환하는 메소드
    /// </summary>
    public void EnterKeywordSetting()
    {
        // 기존 UI 비활성화
        ActiveMapUI(false);

        // BookPassR 애니메이션 재생
        bookAnimator.SetTrigger("shouldTurnPageToLeft");

        // 키워드 세팅 UI 활성화
        ActiveKeywordSettingUI(true);

        // yield return new WaitForSeconds(1.5f);

        // 오리지널 덱 키워드 프리팹 인스턴스화
        ShowOriginalDeckInfo();
    }


    /// <summary>
    /// 전장에 돌입하면 BookPassR 애니메이션 재생, 전투 UI 활성화하는 메소드
    /// </summary>
    public IEnumerator EnterBattleField()
    {
        // BookPassR 애니메이션 재생
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
    }

    /// <summary>
    /// 오리지널 덱의 Support, Main 키워드 프리팹을 인스턴스화하는 메소드
    /// </summary>
    private void ShowOriginalDeckInfo()
    {
        GameObject keywordTemp;
        Vector3 scaleTemp = new Vector3(1, 1, 1);

        // Support 키워드 인스턴스화
        for (int i = 0; i < originalDeck.GetSupDeckSize(); i++)
        {
            keywordTemp = Instantiate(originalDeck.SupportDeck[i], keywordSettingUI.transform.GetChild(0).Find("OriginalSupportDeck"));
            keywordTemp.transform.DOScale(scaleTemp, 0.0f);
        }

        // Main 키워드 인스턴스화
        for (int i = 0; i < originalDeck.GetMainDeckSize(); i++)
        {
            keywordTemp = Instantiate(originalDeck.MainDeck[i], keywordSettingUI.transform.GetChild(0).Find("OriginalMainDeck"));
            keywordTemp.transform.DOScale(scaleTemp, 0.0f);
        }
    }


    // UI Active 함수들 ================================

    /// <summary>
    /// 맵 UI 활성화 여부를 일괄 관리하는 메소드
    /// </summary>
    /// <param name="enableOrNot">UI 캔버스 SetActive() 여부</param>
    private void ActiveMapUI(bool enableOrNot)
    {
        for (int i = 0; i < mapUI.Count; i++)
        {
            mapUI[i].SetActive(enableOrNot);
        }
    }

    /// <summary>
    /// 전투 관련 기능 및 UI 활성화 여부를 일괄 관리하는 메소드
    /// </summary>
    /// <param name="enableOrNot">UI 캔버스 SetActive() 여부</param>
    private void ActiveCombatFunctionAndUI(bool enableOrNot)
    {
        for (int i = 0; i < combatFunctionAndUI.Count; i++)
        {
            combatFunctionAndUI[i].SetActive(enableOrNot);
        }
    }

    /// <summary>
    /// 키워드 세팅 창 UI 활성화 여부를 일괄 관리하는 메소드
    /// </summary>
    /// <param name="enableOrNot">UI 캔버스 SetActive() 여부</param>
    public void ActiveKeywordSettingUI(bool enableOrNot)
    {
        keywordSettingUI.SetActive(enableOrNot);
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
