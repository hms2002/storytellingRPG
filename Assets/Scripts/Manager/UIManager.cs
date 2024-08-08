using DG.Tweening;
using Map;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 게임 운영에 필요한 UI 관리를 담당
/// <para> 맵 UI, 덱 세팅 UI, 전투 UI 제어 </para>
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("맵")]
    [SerializeField] private List<GameObject> mapBackground;        // 전투맵 배경           ※후추

    [Header("키워드 세팅 윈도우")]
    [SerializeField] private GameObject keywordSettingWindow;       // 키워드 세팅 윈도우 객체

    [Header("전투 기능 및 UI")]
    [SerializeField] private List<GameObject> combatFunctionAndUI;  // 전투 관련 모든 UI를 담는 리스트
    [SerializeField] private GameObject combatKeywordUI;

    [Header("전투 백그라운드")]
    [SerializeField] private GameObject combatBackground;

    [Header("상점 UI")]
    [SerializeField] private GameObject ShopUI;

    [Header("아이콘")]
    [SerializeField] private GameObject theEndIcon;                 //게임 오버 아이콘

    private bool _isBattleOver = false;                             // 전투 종료 여부
    public bool isBattleOver { get => _isBattleOver; set => _isBattleOver = value; }


    /*==================================================================================================================================*/


    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// 버튼 클릭 불가 연출을 재생한다.
    /// </summary>
    /// <param name="gameObject">적용시킬 오브젝트를 넘겨준다.</param>
    public void ButtonClicklessFeedback(GameObject gameObject)
    {
        // 버튼 컴포넌트 비활성화
        gameObject.GetComponent<Button>().enabled = false;

        // 0.3초 뒤 버튼 컴포넌트 활성화
        DOVirtual.DelayedCall(0.3f, () => gameObject.GetComponent<Button>().enabled = true);

        // 좌우 횡이동 반복 연출 표현
        gameObject.transform.DOPunchPosition(new Vector3(10, 0, 0), 0.3f, 10, 1);
    }

    /// <summary>
    /// 키워드를 보이지 않게 만든다.
    /// </summary>
    /// <param name="keyword">안보이게 만들 키워드를 넘겨준다.</param>
    public void MakeKeywordInvisible(GameObject keyword)
    {
        // 키워드의 버튼 컴포넌트 비활성화
        keyword.GetComponent<Button>().enabled = false;

        // 키워드의 이미지 컴포넌트 비활성화
        keyword.GetComponent<Image>().enabled = false;

        // 키워드 하위 오브젝트 전체 비활성화
        for (int i = 0; i < keyword.transform.childCount; i++)
        {
            keyword.transform.GetChild(i).gameObject.SetActive(false);
        }
/*
        foreach (GameObject childObject in keyword.transform)
        {
            childObject.SetActive(false);
        }*/
    }

    // UI Active 함수들 ================================

    /// <summary>
    /// 맵 UI 활성화 상태를 일괄 관리한다.
    /// </summary>
    /// <param name="enableOrDisable"></param>
    public void ActiveMapUI(bool enableOrDisable)
    {
        MapState.InstanceMap.mapObj.SetActive(enableOrDisable);
    }

    /// <summary>
    /// 전투 관련 기능 및 UI 활성화 상태를 일괄 관리한다.
    /// </summary>
    /// <param name="enableOrDisable">UI 캔버스 SetActive() 여부</param>
    public void ActiveCombatFunctionAndUI(bool enableOrDisable)
    {
        for (int i = 0; i < combatFunctionAndUI.Count; i++)
        {
            combatFunctionAndUI[i].SetActive(enableOrDisable);
        }
        combatBackground.SetActive(enableOrDisable);
    }

    /// <summary>
    /// 전투중 키워드 UI 활성화 상태를 일괄 관리한다.
    /// </summary>
    /// <param name="enableOrDisable">전투 키워드UI SetActive() 여부</param>
    public void ActiveCombatKeywordUI(bool enableOrDisable)
    {
        combatKeywordUI.SetActive(enableOrDisable);
    }

    /// <summary>
    /// 키워드 세팅 창 UI 활성화 상태를 일괄 관리한다.
    /// </summary>
    /// <param name="enableOrDisable">UI 캔버스 SetActive() 여부</param>
    public void ActiveKeywordSettingUI(bool enableOrDisable)
    {
        keywordSettingWindow.SetActive(enableOrDisable);
    }

    /// <summary>
    /// The End 아이콘을 활성화 혹은 비활성화하는 메소드ㅋㅋ
    /// </summary>
    /// <param name="enableOrDisable">아이콘 SetActive() 여부</param>
    public void ActiveTheEndIcon(bool enableOrDisable)
    {
        theEndIcon.SetActive(enableOrDisable);
    }

    /// <summary>
    /// 상점 UI의 활성화 상태를 일괄 관리한다.
    /// </summary>
    /// <param name="enableOrDisable">상점UI SetActive() 여부</param>
    public void ActiveShopUI(bool enableOrDisable)
    {
        ShopUI.SetActive(enableOrDisable);
    }
};
