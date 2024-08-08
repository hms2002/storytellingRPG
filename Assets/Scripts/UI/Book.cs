using DG.Tweening;
using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 책에 들어갈 모든 기능을 담당
/// <para> 북마크, 책 애니메이션 기능 제어 </para>
/// </summary>
public class Book : MonoBehaviour
{
    public static Book instance;

    [Header("책갈피")]
    [SerializeField] private List<GameObject> bookmarks;                // 북마크 버튼들
    [SerializeField] private List<GameObject> originalSupMainDeckUI;    // 키워드 세팅의 오리지널 서포트, 메인 덱 UI를 담을 리스트

    [Header("플레이어 오리지널 덱")]
    [SerializeField] private Deck originalDeck;         // 플레이어가 갖고 있는 오리지널 덱

    private Animator _bookAnimator;                     // 책 애니메이터
    public Animator bookAnimator => _bookAnimator;

    [Header("페이지 전환 애니메이션 이후 UI 활성화 딜레이 시간")]
    [SerializeField] private float _UIActiveDelay = 0.9f;
    public float UIActiveDelay { get => _UIActiveDelay; }

    private bool _wasOriginalDeckInstanciate = false;   // 오리지널 덱 키워드들의 인스턴스화 여부
    public bool wasOriginalDeckInstanciate { get => _wasOriginalDeckInstanciate; set => _wasOriginalDeckInstanciate = value; }


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

    private void Start()
    {
        _bookAnimator = GetComponent<Animator>();
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
        UIManager.instance.ActiveKeywordSettingUI(false);
        /* 후에 추가될 UI들 이 아래로 SetActive(false) 추가 요망 */

        //BookPassL 애니메이션 재생
        bookAnimator.SetTrigger("turnPageToLeft");

        // 북마크 - 맵 UI 활성화
        DOVirtual.DelayedCall(UIActiveDelay, () => UIManager.instance.ActiveMapUI(true));
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
        UIManager.instance.ActiveMapUI(false);
        /* 후에 추가될 UI들 이 아래로 SetActive(false) 추가 요망 */

        // BookPassR 애니메이션 재생
        bookAnimator.SetTrigger("turnPageToRight");

        // 북마크 - 키워드 세팅 UI 활성화
        DOVirtual.DelayedCall(UIActiveDelay, () => UIManager.instance.ActiveKeywordSettingUI(true));

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
        bookAnimator.SetTrigger("turnPageToRight");

        // 전투 기능 및 UI 활성화
        DOVirtual.DelayedCall(UIActiveDelay, () => UIManager.instance.ActiveCombatFunctionAndUI(true));
    }

    /// <summary>
    /// 전장에서 벗어나면 Player 제거, 전투 UI 비활성화, BookPassR 애니메이션 재생하는 메소드ㅋㅋ
    /// </summary>
    public void GetOutOfBattleField()
    {
        // gameState를 Map으로 전환
        GameManager.instance.gameState = GameState.Map;

        // 전투 관련 캔버스 끄기
        UIManager.instance.ActiveCombatFunctionAndUI(false);
        UIManager.instance.ActiveCombatKeywordUI(false);

        // 책 페이지 오른쪽으로 넘기도록 트리거 발동
        bookAnimator.SetTrigger("turnPageToRight");

        // 2초 뒤 맵 북마크로 이동
        DOVirtual.DelayedCall(2.0f, EnterMap);
    }

    /// <summary>
    /// 오리지널 덱의 Support, Main 키워드 프리팹을 인스턴스화하는 메소드ㅋㅋ
    /// </summary>
    private void MakeOriginalDeckInfo()
    {
        GameObject keywordTemp;     // 인스턴스화된 키워드를 잠시 담아놓을 변수

        // Support 키워드 인스턴스화 및 설정
        for (int i = 0; i < originalDeck.SupportDeck.Count; i++)
        {
            // i번째 키워드 인스턴스화
            keywordTemp = Instantiate(originalDeck.SupportDeck[i], originalSupMainDeckUI[0].transform);

            // 키워드 SupKeywordBox 오브젝트 활성화
            keywordTemp.transform.Find("SupKeywordBox").gameObject.SetActive(true);

            // 키워드 버튼 컴포넌트 비활성화
            keywordTemp.GetComponent<Button>().interactable = false;
        }

        // Main 키워드 인스턴스화 및 설정
        for (int i = 0; i < originalDeck.MainDeck.Count; i++)
        {
            // i번째 키워드 인스턴스화
            keywordTemp = Instantiate(originalDeck.MainDeck[i], originalSupMainDeckUI[1].transform);

            // 키워드 MainKeywordBox 오브젝트 활성화
            keywordTemp.transform.Find("MainKeywordBox").gameObject.SetActive(true);

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
        for (int i = 0; i < originalDeck.SupportDeck.Count; i++)
        {
            // OriginalSupportDeck 하위 객체 참조
            keywordTemp = originalSupMainDeckUI[0].transform.GetChild(i).gameObject;

            // 참조한 하위 객체 제거
            Destroy(keywordTemp);
        }

        // OriginalMainDeck 그리드 레이아웃 그룹 
        for (int i = 0; i < originalDeck.MainDeck.Count; i++)
        {
            // OriginalMainDeck 하위 객체 참조
            keywordTemp = originalSupMainDeckUI[1].transform.GetChild(i).gameObject;

            // 참조한 하위 객체 제거
            Destroy(keywordTemp);
        }

        // 오리지널 덱 UI Destroy되었으니 false
        wasOriginalDeckInstanciate = false;
    }


    // 사운드 출력 함수들 ================================

    /// <summary>
    /// 페이지를 오른쪽으로 넘길때 사운드를 재생한다.
    /// </summary>
    public void TurnPageRightSound()
    {
        AudioManager.instance.PlaySound("Book", "페이지_넘기기");
    }

    /// <summary>
    /// 페이지를 왼쪽으로 넘길때 사운드를 재생한다.
    /// </summary>
    public void TurnPageLeftSound()
    {
        AudioManager.instance.PlaySound("Book", "페이지_돌아가기");
    }

    /// <summary>
    /// 페이지를 북마크로 넘길때 사운드를 재생한다.
    /// </summary>
    public void BookMarkSound()
    {
        AudioManager.instance.PlaySound("Book", "책갈피_넘기기");
    }
}
