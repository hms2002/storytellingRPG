using DG.Tweening;
using Map;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private List<GameObject> bookmarks;                        // 북마크 버튼들
    [SerializeField] private List<GameObject> originalSupMainDeckUI;            // 키워드 세팅의 오리지널 서포트, 메인 덱 UI를 담을 리스트

    [Header("플레이어 오리지널 덱")]
    [SerializeField] private Deck originalDeck;             // 플레이어가 갖고 있는 오리지널 덱

    [Header("접힌 페이지 오브젝트")]
    [SerializeField] private List<GameObject> foldedPages;  // Keyword Setting 캔버스 하위의 FoldedPageL,R 오브젝트를 담는 리스트

    private Animator _bookAnimator;                         // 책 애니메이터
    public Animator bookAnimator => _bookAnimator;

    [SerializeField] private List<GameObject> supKeywordsForDisplay = new List<GameObject>();    // Keyword Setting의 Support 키워드들을 담아둘 리스트
    [SerializeField] private List<GameObject> mainKeywordsForDisplay = new List<GameObject>();   // Keyword Setting의 Main 키워드들을 담아둘 리스트

    [SerializeField] private int _keywordSettingPage = 1;                    // Keyword Setting의 현재 페이지를 담는 변수
    public int keywordSettingPage
    {
        get { return _keywordSettingPage; }
        set
        {
            _keywordSettingPage = value;

            if (_keywordSettingPage <= 0)
            {
                _keywordSettingPage = 1;
            }
        }
    }

    [Header("페이지 전환 애니메이션 이후 UI 활성화 딜레이 시간")]
    [SerializeField] private float _uIActiveDelay = 0.9f;
    public float uIActiveDelay { get => _uIActiveDelay; }

    private bool _wasOriginalDeckInstanciate = false;       // 오리지널 덱 키워드들의 인스턴스화 여부
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
    }

    private void Start()
    {
        _bookAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// 북마크 - 맵으로 UI를 전환합니다.
    /// </summary>
    public void EnterMap()
    {
        // gameState가 Map, Battle, Shop이면 return
        if (GameManager.instance.gameState == GameState.Map     ||
            GameManager.instance.gameState == GameState.Battle  ||
            GameManager.instance.gameState == GameState.Shop ||
            GameManager.instance.gameState == GameState.Event)   return;

        // Map 버튼 클릭 시 1초동안 비활성화
        foreach (GameObject g in bookmarks)
        {
            g.GetComponent<Button>().enabled = false;
        }
        DOVirtual.DelayedCall(1.0f, () =>
        {
            foreach (GameObject g in bookmarks)
            {
                g.GetComponent<Button>().enabled = true;
            }
        });

        // gameState를 Map으로 전환
        GameManager.instance.gameState = GameState.Map;

        // 타 UI 전부 비활성화
        UIManager.instance.ActiveKeywordSettingUI(false);
        UIManager.instance.ActiveCombatFunctionAndUI(false);
        UIManager.instance.ActiveCombatKeywordUI(false);    
        UIManager.instance.ActiveEventUI(false);
        UIManager.instance.ActiveIsland(false);
        /* 후에 추가될 UI들 이 아래로 SetActive(false) 추가 요망 */
        TextManager.instance.Text.text = string.Empty;
        //BookPassL 애니메이션 재생
        bookAnimator.SetTrigger("turnPageToLeft");

        // 북마크 - 맵 UI 활성화
        DOVirtual.DelayedCall(uIActiveDelay, () => UIManager.instance.ActiveMapUI(true));
    }

    #region Keyword Setting 관련 함수들
    /// <summary>
    /// 북마크 - 키워드 세팅으로 UI를 전환합니다.
    /// </summary>
    public void EnterKeywordSetting()
    {
        // gameState가 KeywordSetting, Battle, Shop이면 return
        if (GameManager.instance.gameState == GameState.KeywordSetting ||
            GameManager.instance.gameState == GameState.Battle ||
            GameManager.instance.gameState == GameState.Shop ||
            GameManager.instance.gameState == GameState.Event) { return; }

        // KeywordSetting 버튼 클릭 시 1초동안 비활성화
        foreach (GameObject g in bookmarks)
        {
            g.GetComponent<Button>().enabled = false;
        }
        DOVirtual.DelayedCall(1.0f, () =>
        {
            foreach (GameObject g in bookmarks)
            {
                g.GetComponent<Button>().enabled = true;
            }
        });

        // gameState를 KeywordSetting으로 전환
        GameManager.instance.gameState = GameState.KeywordSetting;

        // 오리지널 덱 UI 제거
        DestroyOriginalDeckInfo();

        // 타 UI 전부 비활성화
        UIManager.instance.ActiveMapUI(false);
        /* 후에 추가될 UI들 이 아래로 SetActive(false) 추가 요망 */

        // BookPassR 애니메이션 재생
        bookAnimator.SetTrigger("turnPageToRight");

        // 북마크 - 키워드 세팅 UI 활성화
        DOVirtual.DelayedCall(uIActiveDelay, () => UIManager.instance.ActiveKeywordSettingUI(true));

        // 오리지널 덱 키워드가 인스턴스화 되어있지 않다면
        if (!wasOriginalDeckInstanciate)
        {
            // 오리지널 덱 키워드 프리팹 인스턴스화
            MakeOriginalDeckInfo(Keyword.ButtonType.Display);
        }
    }

    /// <summary>
    /// 키워드 세팅으로 UI를 전환합니다.
    /// </summary>
    /// <param name="thisType">키워드의 사용 용도를 입력합니다.</param>
    public void EnterKeywordSetting(Keyword.ButtonType thisType)
    {
        // 오리지널 덱 UI 제거
        DestroyOriginalDeckInfo();

        // 페이지 넘기기 애니메이션
        bookAnimator.SetTrigger("turnPageToRight");

        // 북마크 - 키워드 세팅 UI 활성화
        DOVirtual.DelayedCall(uIActiveDelay, () => UIManager.instance.ActiveKeywordSettingUI(true));

        // 소지금 표시창 활성화
        DOVirtual.DelayedCall(uIActiveDelay, () => ShopManager.instance.goldPanel.SetActive(true));

        // 오리지널 덱 키워드 프리팹 인스턴스화
        MakeOriginalDeckInfo(thisType);
    }

    /// <summary>
    /// 오리지널 덱의 Support, Main 키워드 프리팹을 인스턴스화한다.
    /// </summary>
    /// <param name="thisType">키워드의 사용 용도를 작성합니다.</param>
    private void MakeOriginalDeckInfo(Keyword.ButtonType thisType)
    {
        keywordSettingPage = 1;

        // 오리지널 Support덱의 키워드 수가 10개 초과라면
        if (originalDeck.SupportDeck.Count > 10)
        {
            // 접힌 페이지R 버튼 활성화
            foldedPages[1].SetActive(true);

            // Support덱 인스턴스화
            MakeKeywordsAndSetting(WhatDeck.SupportDeck, thisType);

            // 11번째 Support 키워드부터 오리지널 Support덱 길이만큼 반복
            for (int i = 10; i < originalDeck.SupportDeck.Count; i++)
            {
                // Support 키워드 비활성화
                supKeywordsForDisplay[i].SetActive(false);
            }
        }
        else
        {
            // Support덱 인스턴스화
            MakeKeywordsAndSetting(WhatDeck.SupportDeck, thisType);
        }

        // 오리지널 Main덱의 키워드 수가 10개 초과라면
        if (originalDeck.MainDeck.Count > 10)
        {
            // FoldedPageR이 비활성화 상태라면 활성화
            if (!foldedPages[1].activeSelf) foldedPages[1].SetActive(true);

            // Main덱 인스턴스화
            MakeKeywordsAndSetting(WhatDeck.MainDeck, thisType);

            // 11번째 Main 키워드부터 오리지널 Main덱 길이만큼 반복
            for (int i = 10; i < originalDeck.MainDeck.Count; i++)
            {
                // Main 키워드 비활성화
                mainKeywordsForDisplay[i].SetActive(false);
            }
        }
        else
        {
            // Main덱 인스턴스화
            MakeKeywordsAndSetting(WhatDeck.MainDeck, thisType);
        }

        // 오리지널 덱 UI 인스턴스화되었으니 true
        wasOriginalDeckInstanciate = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="thisDeck"></param>
    private void MakeKeywordsAndSetting(WhatDeck thisDeck, Keyword.ButtonType thisType)
    {
        switch (thisDeck)
        {
            case WhatDeck.SupportDeck:

                // Support 키워드 인스턴스화 및 설정
                for (int i = 0; i < originalDeck.SupportDeck.Count; i++)
                {
                    // i번째 키워드 인스턴스화
                    supKeywordsForDisplay.Add(Instantiate(originalDeck.SupportDeck[i], originalSupMainDeckUI[0].transform));

                    // 키워드 SupKeywordBox 오브젝트 활성화
                    supKeywordsForDisplay[i].transform.Find("SupKeywordBox").gameObject.SetActive(true);

                    // 키워드 버튼타입 적용
                    supKeywordsForDisplay[i].GetComponent<KeywordSup>().buttonType = thisType;

                    // 키워드 버튼타입이 Display라면 버튼 interactable 비활성화
                    if (supKeywordsForDisplay[i].GetComponent<KeywordSup>().buttonType == Keyword.ButtonType.Display)
                        supKeywordsForDisplay[i].GetComponent<Button>().interactable = false;

                    // 키워드 각조 조절
                    float randomAngle = Random.Range(-3.0f, 3.0f);
                    supKeywordsForDisplay[i].transform.rotation = Quaternion.Euler(0.0f, 0.0f, randomAngle);
                    supKeywordsForDisplay[i].transform.GetChild(0).rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                }

                break;

            case WhatDeck.MainDeck:

                // Main 키워드 인스턴스화 및 설정
                for (int i = 0; i < originalDeck.MainDeck.Count; i++)
                {
                    // i번째 키워드 인스턴스화
                    mainKeywordsForDisplay.Add(Instantiate(originalDeck.MainDeck[i], originalSupMainDeckUI[1].transform));

                    // 키워드 MainKeywordBox 오브젝트 활성화
                    mainKeywordsForDisplay[i].transform.Find("MainKeywordBox").gameObject.SetActive(true);

                    // 키워드 버튼타입 적용
                    mainKeywordsForDisplay[i].GetComponent<KeywordMain>().buttonType = thisType;

                    // 키워드 버튼타입이 Display라면 버튼 interactable 비활성화
                    if (mainKeywordsForDisplay[i].GetComponent<KeywordMain>().buttonType == Keyword.ButtonType.Display)
                        mainKeywordsForDisplay[i].GetComponent<Button>().interactable = false;

                    // 키워드 각조 조절
                    mainKeywordsForDisplay[i].transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(-3.0f, 3.0f));
                    mainKeywordsForDisplay[i].transform.GetChild(0).rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                }

                break;
        }
    }

    /// <summary>
    /// FoldedPageL 버튼 클릭 시 Keyword Setting 페이지를 왼쪽으로 넘긴다.
    /// </summary>
    public void TurnKeywordSettingPageToLeft()
    {
        int startIndex      = (keywordSettingPage - 1) * 10;                                    // SetActive() 돌릴 리스트 인덱스 시작점
        int endIndexForSup  = Mathf.Min(keywordSettingPage * 10, supKeywordsForDisplay.Count);  // SetActive() 돌릴 Support 리스트 인덱스 끝점
        int endIndexForMain = Mathf.Min(keywordSettingPage * 10, mainKeywordsForDisplay.Count); // SetActive() 돌릴 Support 리스트 인덱스 끝점

        // Support - (이전 페이지 값 -1) X 10부터 이전 페이지 값 X 10까지 반복
        for (int i = startIndex; i < endIndexForSup; i++)
        {
            // i가 Support 리스트 길이보다 작다면 이전 페이지 Support 키워드들 비활성화
            if (i < supKeywordsForDisplay.Count) supKeywordsForDisplay[i].SetActive(false);
        }

        // Main - (이전 페이지 값 -1) X 10부터 이전 페이지 값 X 10까지 반복
        for (int i = startIndex; i < endIndexForMain; i++)
        {
            // i가 Main 리스트 길이보다 작다면 이전 페이지 Main 키워드들 비활성화
            if (i < mainKeywordsForDisplay.Count)  mainKeywordsForDisplay[i].SetActive(false);
        }

        // 접힌 페이지 UI 비활성화
        foreach (GameObject foldedPage in foldedPages)
        {
            foldedPage.SetActive(false);
        }

        // BookTurnR 애니메이션 재생
        bookAnimator.SetTrigger("turnPageToLeft");

        // 페이지 변수값 1 감소
        keywordSettingPage--;

        // 시작과 끝 인덱스 변수 설정
        startIndex = (keywordSettingPage - 1) * 10;
        endIndexForSup = Mathf.Min(keywordSettingPage * 10, supKeywordsForDisplay.Count);
        endIndexForMain = Mathf.Min(keywordSettingPage * 10, mainKeywordsForDisplay.Count);

        // 
        StartCoroutine(ActivateKeywordsWithDelay(supKeywordsForDisplay, startIndex, endIndexForSup, uIActiveDelay));
        StartCoroutine(ActivateKeywordsWithDelay(mainKeywordsForDisplay, startIndex, endIndexForMain, uIActiveDelay));

        // 
        if (foldedPages[1].activeSelf == false)
            DOVirtual.DelayedCall(uIActiveDelay, () => foldedPages[1].SetActive(true));

        // 페이지 값이 1이면 FoldedPageL 오브젝트 비활성화
        if (keywordSettingPage == 1) foldedPages[0].SetActive(false);
        else if (keywordSettingPage > 1) DOVirtual.DelayedCall(uIActiveDelay, () => foldedPages[0].SetActive(true));
    }

    /// <summary>
    /// FoldedPageR 버튼 클릭 시 Keyword Setting 페이지를 오른쪽으로 넘긴다.
    /// </summary>
    public void TurnKeywordSettingPageToRight()
    {
        int startIndex      = (keywordSettingPage - 1) * 10;                                    // SetActive() 돌릴 리스트 인덱스 시작점
        int endIndexForSup  = Mathf.Min(keywordSettingPage * 10, supKeywordsForDisplay.Count);  // SetActive() 돌릴 Support 리스트 인덱스 끝점
        int endIndexForMain = Mathf.Min(keywordSettingPage * 10, mainKeywordsForDisplay.Count); // SetActive() 돌릴 Main 리스트 인덱스 끝점

        // Support - (이전 페이지 값 -1) X 10부터 이전 페이지 값 X 10까지 반복
        for (int i = startIndex; i < endIndexForSup; i++)
        {
            // i가 Support 리스트 길이보다 작다면 이전 페이지 Support 키워드들 비활성화
            if (i < supKeywordsForDisplay.Count) supKeywordsForDisplay[i].SetActive(false);
        }

        // Main - (이전 페이지 값 -1) X 10부터 이전 페이지 값 X 10까지 반복
        for (int i = startIndex; i < endIndexForMain; i++)
        {
            // i가 Main 리스트 길이보다 작다면 이전 페이지 Main 키워드들 비활성화
            if (i < mainKeywordsForDisplay.Count) mainKeywordsForDisplay[i].SetActive(false);
        }

        // 접힌 페이지 UI 비활성화
        foreach (GameObject foldedPage in foldedPages)
        {
            foldedPage.SetActive(false);
        }

        // BookTurnR 애니메이션 재생
        bookAnimator.SetTrigger("turnPageToRight");

        // 페이지 변수값 1 증가
        keywordSettingPage++;

        // 시작과 끝 인덱스 변수 설정
        startIndex      = (keywordSettingPage - 1) * 10;
        endIndexForSup  = Mathf.Min(keywordSettingPage * 10, supKeywordsForDisplay.Count);
        endIndexForMain = Mathf.Min(keywordSettingPage * 10, mainKeywordsForDisplay.Count);

        // FoldedPageL 오브젝트가 비활성화 상태라면 활성화
        if (!foldedPages[0].activeSelf)
            DOVirtual.DelayedCall(uIActiveDelay, () => foldedPages[0].SetActive(true));

        // 
        StartCoroutine(ActivateKeywordsWithDelay(supKeywordsForDisplay, startIndex, endIndexForSup, uIActiveDelay));
        StartCoroutine(ActivateKeywordsWithDelay(mainKeywordsForDisplay, startIndex, endIndexForMain, uIActiveDelay));

        // endIndexForSup 값이 supKeywordsForDisplay 리스트 길이와 같고, endIndexForMain 값이 mainKeywordsForDisplay 리스트 길이와 같다면
        if (endIndexForSup == supKeywordsForDisplay.Count && endIndexForMain == mainKeywordsForDisplay.Count)
        {
            // FoldedPageR 오브젝트 비활성화
            foldedPages[1].SetActive(false);
        }
        else if (endIndexForSup < supKeywordsForDisplay.Count || endIndexForMain < mainKeywordsForDisplay.Count)
        {
            DOVirtual.DelayedCall(uIActiveDelay, () => foldedPages[1].SetActive(true));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="keywordsList"></param>
    /// <param name="startIndex"></param>
    /// <param name="endIndex"></param>
    /// <param name="delay"></param>
    /// <returns></returns>
    private IEnumerator ActivateKeywordsWithDelay(List<GameObject> keywordsList, int loopStart, int loopEnd, float delay)
    {
        yield return new WaitForSeconds(delay);

        for (int i = loopStart; i < loopEnd; i++)
        {
            keywordsList[i].SetActive(true);
        }
    }

    /// <summary>
    /// 오리지널 덱의 Support, Main 키워드 오브젝트를 제거한다.
    /// </summary>
    public void DestroyOriginalDeckInfo()
    {
        // 오리지널 덱 키워드를 인스턴스화하지 않았다면 반환
        if (!wasOriginalDeckInstanciate) return;

        // supKeywordsForDisplay 리스트의 길이만큼 반복
        for (int i = 0; i < supKeywordsForDisplay.Count; i++)
        {
            // Support 키워드 제거
            Destroy(supKeywordsForDisplay[i]);
        }

        // mainKeywordsForDisplay 리스트의 길이만큼 반복
        for (int i = 0; i < mainKeywordsForDisplay.Count; i++)
        {
            // Main 키워드 제거
            Destroy(mainKeywordsForDisplay[i]);
        }

        // 
        supKeywordsForDisplay.Clear();
        mainKeywordsForDisplay.Clear();

        // 오리지널 덱 UI Destroy되었으니 false
        wasOriginalDeckInstanciate = false;

        // keyword Setting 페이지 변수 초기화
        keywordSettingPage = 1;

        // FoldedPageL 오브젝트 비활성화
        if(foldedPages[0] != null)
            foldedPages[0].SetActive(false);

        // Folded Page 오브젝트 전부 비활성화
        foreach (GameObject foldedPage in foldedPages)
        {
            if (foldedPage != null)
                foldedPage.SetActive(false);
        }

    }
    #endregion

    /// <summary>
    /// 전장에 돌입하면 BookPassR 애니메이션 재생, 전투 UI를 활성화합니다.
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
        DOVirtual.DelayedCall(uIActiveDelay, () => UIManager.instance.ActiveCombatFunctionAndUI(true));
    }

    /// <summary>
    /// 전장에서 벗어나면 Player 제거, 전투 UI 비활성화, BookPassR 애니메이션 재생하는 메소드입니다.
    /// </summary>
    public void GetOutOfBattleField()
    {
        GameManager.instance.gameState = GameState.EndBattle;

        FightManager.currentTurn = 0;

        // 전투 관련 캔버스 끄기
        UIManager.instance.ActiveCombatFunctionAndUI(false);
        UIManager.instance.ActiveCombatKeywordUI(false);
        UIManager.instance.ActiveIsland(false); 
        // 책 페이지 오른쪽으로 넘기도록 트리거 발동
/*        bookAnimator.SetTrigger("turnPageToRight");
*/
        // 2초 뒤 맵 북마크로 이동
        DOVirtual.DelayedCall(0.5f, EnterMap);
    }

    /// <summary>
    /// Rest 노드 애니메이션 및 생성 코드
    /// </summary>
    public void EnterRestField()
    {
        // gameState를 Battle로 전환
        GameManager.instance.gameState = GameState.Rest;

        // BookPassR 애니메이션 재생
        bookAnimator.SetTrigger("turnPageToRight");

        // 전투 기능 및 UI 활성화
        DOVirtual.DelayedCall(uIActiveDelay, () => {
            // UI 활성화
            UIManager.instance.ActiveEventUI(true);
            EventManager.instance.ShowEvent(EventDatabase.eventDatas.rest);
            if (StageManager.instance.nowStageState == StageState.Sea)
        {
            UIManager.instance.ActiveIsland(true);
        }
        });
    }

    public void EnterEventField()
    {
        // gameState를 Event로 전환
        GameManager.instance.gameState = GameState.Event;

        // BookPassR 애니메이션 재생
        bookAnimator.SetTrigger("turnPageToRight");

        // 전투 기능 및 UI 활성화
        DOVirtual.DelayedCall(uIActiveDelay, () =>
        {
            // UI 활성화
            UIManager.instance.ActiveEventUI(true);

            // 딜레이 후 switch문 실행
            switch (StageManager.instance.nowStageState)
            {
                case StageState.Forest:
                    EventManager.instance.ShowEvent(EventDatabase.eventDatas.stage1EventList[GameManager.instance.eventIndex]);
                    break;
                case StageState.Cave:
                    EventManager.instance.ShowEvent(EventDatabase.eventDatas.stage2EventList[GameManager.instance.eventIndex]);
                    break;
                case StageState.Sea:
                    EventManager.instance.ShowEvent(EventDatabase.eventDatas.stage3EventList[GameManager.instance.eventIndex]);
                    break;
                case StageState.MagicTower:
                    EventManager.instance.ShowEvent(EventDatabase.eventDatas.stage4EventList[GameManager.instance.eventIndex]);
                    break;
            }
        });

    }
    public void EnterTreasureField()
    {
        // gameState를 Event로 전환
        GameManager.instance.gameState = GameState.Event;

        // BookPassR 애니메이션 재생
        bookAnimator.SetTrigger("turnPageToRight");

        // 전투 기능 및 UI 활성화
        DOVirtual.DelayedCall(uIActiveDelay, () =>
        {
            // UI 활성화
            UIManager.instance.ActiveEventUI(true);

            EventManager.instance.ShowTreasure();
            if(StageManager.instance.nowStageState == StageState.Sea)
            {
                UIManager.instance.ActiveIsland(true);
            }
        });

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
