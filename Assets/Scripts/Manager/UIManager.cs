using DG.Tweening;
using Map;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 게임 운영에 필요한 UI 관리를 담당 | 
/// <para> 맵 UI, 덱 세팅 UI, 전투 UI, 책 애니메이션 제어 </para>
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("맵")]
    [SerializeField] private List<GameObject> mapBackground;            // 전투맵 배경
 
    [Header("책갈피")]
    [SerializeField] private List<GameObject> bookmarks;                // 북마크 버튼들
    [SerializeField] private GameObject keywordSettingWindow;           // 키워드 세팅 윈도우 객체
    [SerializeField] private List<GameObject> originalSupMainDeckUI;    // 키워드 세팅의 오리지널 서포트, 메인 덱 UI를 담을 리스트

    [Header("전투 기능 및 UI")]
    [SerializeField] private List<GameObject> combatFunctionAndUI;      // 전투 관련 모든 UI를 담는 리스트
    [SerializeField] private GameObject combatKeywordUI;
    [SerializeField] private Deck originalDeck;             // 플레이어가 갖고 있는 오리지널 덱

    [Header("전투 백그라운드")]
    [SerializeField] private GameObject combatBackground;

    [Header("책")]
    [SerializeField] private Animator bookAnimator;         // 책 애니메이터

    [Header("아이콘")]
    [SerializeField] private GameObject theEndIcon;         //게임 오버 아이콘

    private bool _isBattleOver = false;                     // 전투 종료 여부
    public bool isBattleOver { get => _isBattleOver; set => _isBattleOver = value; }

    private bool _wasOriginalDeckInstanciate = false;       // 오리지널 덱 키워드들의 인스턴스화 여부
    public bool wasOriginalDeckInstanciate { get => _wasOriginalDeckInstanciate; set => _wasOriginalDeckInstanciate = value; }


    /*==================================================================================================================================*/


    private void Awake()
    {
        // 싱글톤 구조 보강
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// 북마크 - 맵으로 UI 전환하는 메소드ㅋㅋ
    /// </summary>
    public void EnterMap()
    {
        // gameState가 Map이거나 Battle이면 return
        if (GameManager.instance.gameState == GameState.Map || GameManager.instance.gameState == GameState.Battle) return;

        // Map 버튼 클릭 시 1초동안 비활성화
        bookmarks[1].GetComponent<Button>().enabled = false;
        DOVirtual.DelayedCall(1.0f, () => bookmarks[1].GetComponent<Button>().enabled = true);

        // gameState를 Map으로 전환
        GameManager.instance.gameState = GameState.Map;

        // 타 UI 전부 비활성화
        ActiveKeywordSettingUI(false);


        //BookPassL 애니메이션 재생
        bookAnimator.SetTrigger("shouldTurnPageToLeft");

        // 북마크 - 맵 UI 활성화
        DOVirtual.DelayedCall(0.9f, MapState.InstanceMap.OpenMap);
        
    }

    /// <summary>
    /// 북마크 - 키워드 세팅으로 UI 전환하는 메소드ㅋㅋ
    /// </summary>
    public void EnterKeywordSetting()
    {
        // gameState가 KeywordSetting이거나 Battle이면 return
        if (GameManager.instance.gameState == GameState.KeywordSetting || GameManager.instance.gameState == GameState.Battle) return;

        // KeywordSetting 버튼 클릭 시 1초동안 비활성화
        bookmarks[0].GetComponent<Button>().enabled = false;
        DOVirtual.DelayedCall(1.0f, () => bookmarks[0].GetComponent<Button>().enabled = true);

        // gameState를 KeywordSetting으로 전환
        GameManager.instance.gameState = GameState.KeywordSetting;

        // 타 UI 전부 비활성화
        MapState.InstanceMap.CloseMap();


        // BookPassR 애니메이션 재생
        bookAnimator.SetTrigger("shouldTurnPageToRight");

        // 북마크 - 키워드 세팅 UI 활성화
        DOVirtual.DelayedCall(0.9f, () => ActiveKeywordSettingUI(true));

        // 오리지널 덱 키워드가 이미 인스턴스화 되어 있다면
        if (!wasOriginalDeckInstanciate)
        {
            // 오리지널 덱 키워드 프리팹 인스턴스화
            MakeOriginalDeckInfo();
        }
    }


    /// <summary>
    /// 전장에 돌입하면 BookPassR 애니메이션 재생, 전투 UI 활성화하는 메소드ㅋㅋ
    /// </summary>
    public void EnterBattleField()
    {
        // gameState를 Battle로 전환
        GameManager.instance.gameState = GameState.Battle;

        // 오리지널 덱 UI 제거
        DestroyOriginalDeckInfo();

        // BookPassR 애니메이션 재생
        bookAnimator.SetTrigger("shouldTurnPageToRight");

        // 전투 기능 및 UI 활성화
        DOVirtual.DelayedCall(0.9f, () => ActiveCombatFunctionAndUI(true));
    }


    /// <summary>
    /// 전장에서 벗어나면 Player 제거, 전투 UI 비활성화, BookPassR 애니메이션 재생하는 메소드ㅋㅋ
    /// </summary>
    public void GetOutOfBattleField()
    {
        // gameState를 Map으로 전환
        GameManager.instance.gameState = GameState.Map;

        // 전투 관련 캔버스 끄기
        ActiveCombatFunctionAndUI(false);
        ActiveCombatKeywordUI(false);

        // 책 페이지 오른쪽으로 넘기도록 트리거 발동
        bookAnimator.SetTrigger("shouldTurnPageToRight");
    }

    /// <summary>
    /// 오리지널 덱의 Support, Main 키워드 프리팹을 인스턴스화하는 메소드ㅋㅋ
    /// </summary>
    private void MakeOriginalDeckInfo()
    {
        GameObject keywordTemp;     // 인스턴스화된 키워드를 잠시 담아놓을 변수

        // Support 키워드 인스턴스화 및 설정
        for (int i = 0; i < originalDeck.GetSupDeckSize(); i++)
        {
            // i번째 키워드 인스턴스화
            keywordTemp = Instantiate(originalDeck.SupportDeck[i], originalSupMainDeckUI[0].transform);

            // 키워드 Textbox 오브젝트 활성화
            //keywordTemp.transform.Find("Textbox").gameObject.SetActive(true);             * Textbox 오브젝트 추후 적용 예정

            // 키워드 버튼 컴포넌트 비활성화
            keywordTemp.GetComponent<Button>().interactable = false;
        }

        // Main 키워드 인스턴스화 및 설정
        for (int i = 0; i < originalDeck.GetMainDeckSize(); i++)
        {
            // i번째 키워드 인스턴스화
            keywordTemp = Instantiate(originalDeck.MainDeck[i], originalSupMainDeckUI[1].transform);

            // 키워드 Textbox 오브젝트 활성화
            //keywordTemp.transform.Find("Textbox").gameObject.SetActive(true);             * Textbox 오브젝트 추후 적용 예정

            // 키워드 버튼 컴포넌트 비활성화
            keywordTemp.GetComponent<Button>().interactable = false;
        }

        // 오리지널 덱 UI 인스턴스화되었으니 true
        wasOriginalDeckInstanciate = true;
    }

    /// <summary>
    /// 오리지널 덱의 Support, Main 키워드 오브젝트를 제거하는 메소드ㅋㅋ
    /// </summary>
    private void DestroyOriginalDeckInfo()
    {
        // 오리지널 덱 키워드를 인스턴스화하지 않았다면 반환
        if (!wasOriginalDeckInstanciate) return;

        GameObject keywordTemp;     // 제거할 키워드를 잠시 담아놓을 변수

        // OriginalSupportDeck 그리드 레이아웃 그룹 
        for (int i = 0; i < originalDeck.GetSupDeckSize(); i++)
        {
            // OriginalSupportDeck 하위 객체 참조
            keywordTemp = originalSupMainDeckUI[0].transform.GetChild(i).gameObject;

            // 참조한 하위 객체 제거
            Destroy(keywordTemp);
        }

        // OriginalMainDeck 그리드 레이아웃 그룹 
        for (int i = 0; i < originalDeck.GetMainDeckSize(); i++)
        {
            // OriginalMainDeck 하위 객체 참조
            keywordTemp = originalSupMainDeckUI[1].transform.GetChild(i).gameObject;

            // 참조한 하위 객체 제거
            Destroy(keywordTemp);
        }

        // 오리지널 덱 UI Destroy되었으니 false
        wasOriginalDeckInstanciate = false;
    }



    // UI Active 함수들 ================================

    /// <summary>
    /// 전투 관련 기능 및 UI 활성화 여부를 일괄 관리하는 메소드ㅋㅋ
    /// </summary>
    /// <param name="enableOrNot">UI 캔버스 SetActive() 여부</param>
    private void ActiveCombatFunctionAndUI(bool enableOrNot)
    {
        for (int i = 0; i < combatFunctionAndUI.Count; i++)
        {
            combatFunctionAndUI[i].SetActive(enableOrNot);
        }
        combatBackground.SetActive(enableOrNot);
    }

    /// <summary>
    /// 전투중 키워드 UI 생성
    /// </summary>
    /// <param name="enableOrNot">전투 키워드UI SetActive() 여부</param>
    public void ActiveCombatKeywordUI(bool enableOrNot)
    {
        combatKeywordUI.SetActive(enableOrNot);
    }

    /// <summary>
    /// 키워드 세팅 창 UI 활성화 여부를 일괄 관리하는 메소드ㅋㅋ
    /// </summary>
    /// <param name="enableOrNot">UI 캔버스 SetActive() 여부</param>
    public void ActiveKeywordSettingUI(bool enableOrNot)
    {
        keywordSettingWindow.SetActive(enableOrNot);
    }

    /// <summary>
    /// The End 아이콘을 활성화 혹은 비활성화하는 메소드ㅋㅋ
    /// </summary>
    /// <param name="enableOrNot">아이콘 SetActive() 여부</param>
    public void ActiveTheEndIcon(bool enableOrNot)
    {
        theEndIcon.SetActive(enableOrNot);
    }
};
