using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using static UnityEditor.Experimental.GraphView.GraphView;
#endif
/// <summary>
/// 상점 시스템 흐름 제어
/// </summary>
public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;                     // 싱글톤

    [Header("ShopUI 오브젝트")]
    [SerializeField] private Shop shopUI;                     // ShopUI 오브젝트의 Shop 스크립트 컴포넌트

    [Header("Player 오브젝트")]
    [SerializeField] private Actor _player;                  // Player 오브젝트의 Actor 스크립트 컴포넌트
    public Actor player => _player;

    [Header("골드 패널")]
    [SerializeField] private GameObject _goldPanel;         // 소지금 패널 오브젝트
    public GameObject goldPanel => _goldPanel;

    [Header("골드 텍스트")]
    [SerializeField] private TextMeshProUGUI goldHUD;       // 소지금 표시 텍스트

    [Header("키워드 가격 텍스트")]
    [SerializeField] private TextMeshProUGUI keywordPrice;  // 키워드 싯가 표시 텍스트

    [Header("테이블보 태그 오브젝트")]
    [SerializeField] private GameObject tableclothTag;      // 테이블보 책갈피 오브젝트

    [Header("키워드 제거 시 교체될 스프라이트")]
    [SerializeField] private Sprite emptySpaceByErase;      // 키워드 빈자리 스프라이트

    private bool areProductsDisplay;                        // 키워드 진열 여부


    [Space(40)]


    [Header("상품 발주량")]
    [SerializeField] private int _orderVolume = 6;
    public int orderVolume { get => _orderVolume; set => _orderVolume = value; }

    [Header("키워드 당 가격")]
    [SerializeField] private int pricePerKeyword = 75;

    [Header("유물 당 가격")]
    [SerializeField] private List<int> _pricesPerRelic = new List<int>() { 100, 125, 150, 175, 200 };
    public IReadOnlyList<int> pricesPerRelic { get => _pricesPerRelic; }

    [Header("키워드 제거 개당 가격")]
    [SerializeField] private int keywordErasingPrice = 75;


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
    /// 상점 입장 메소드의 플로우입니다.
    /// </summary>
    public void EnterShop()
    {
        // gameState가 Map이거나 Battle이면 return
        if (GameManager.instance.gameState == GameState.Map || GameManager.instance.gameState == GameState.Battle) return;

        // Player UI 비활성화 및 오브젝트 활성화
        player.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        player.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        player.gameObject.SetActive(true);

        // 다른 UI 비활성화
        UIManager.instance.ActiveMapUI(false);
        UIManager.instance.ActiveKeywordSettingUI(false);

        // 페이지 우로 넘기기 애니메이션 재생
        Book.instance.bookAnimator.SetTrigger("turnPageToRight");

        // 상점 UI 활성화 및 플레이어 오브젝트 활성화
        DOVirtual.DelayedCall(Book.instance.uIActiveDelay, () => UIManager.instance.ActiveShopUI(true));

        // 상점 UI 중 바로 사용하지 않는 UI 비활성화
        tableclothTag.SetActive(false);

        // 키워드 가격 표시
        keywordPrice.text = "단돈 " + pricePerKeyword.ToString() + "G!";

        // 플레이어 소지금 표기
        goldHUD.DOText(player.gold + "G", 0.0f);

        // 상품이 진열되어 있지 않다면
        if (!areProductsDisplay)
        {
            // 상품 진열 여부 true
            areProductsDisplay = true;

            // 키워드 발주 및 진열
            shopUI.KeywordProductsDisplay();

            // 유물 발주 및 진열
            shopUI.RelicProductsDisplay();
        }
    }

    /// <summary>
    /// 키워드 구매 메소드의 플로우입니다.
    /// </summary>
    /// <param name="keyword">인스턴스 참조</param>
    /// <param name="keywordType">키워드 타입 참조</param>
    public void PurchaseKeyword(GameObject keyword, Keyword keywordType)
    {
        // 플레이어 소지 금액이 부족하면
        if (player.gold < pricePerKeyword)
        {
            // 버튼 클릭불가 연출 재생
            UIManager.instance.ButtonClicklessFeedback(keyword);

            return;
        }

        // 플레이어의 오리지널덱에 추가
        if (keywordType is KeywordSup) // 키워드 타입이 Support라면
        {
            // GameManager의 모든 Support 키워드 리스트 길이만큼 반복
            for (int i = 0; i < GameManager.instance.allSupKeywordsForPlayer.Count; i++)
            {
                // 오리지널덱의 키워드가 지우고자 하는 키워드와 일치하면
                if (GameManager.instance.allSupKeywordsForPlayer[i].GetComponent<KeywordSup>().keywordName
                    == keyword.GetComponent<KeywordSup>().keywordName)
                {
                    // 보유 골드 HUD에 소지금 차감 및 업데이트
                    UpdateGoldHUD(pricePerKeyword * -1);

                    // 해당 키워드 프리팹을 오리지널 덱 리스트에 추가
                    player.OriginalDeck.AddSupKeywordOnDeck(GameManager.instance.allSupKeywordsForPlayer[i]);
                }
            }
        }
        else if (keywordType is KeywordMain) // 키워드 타입이 Main이라면
        {
            // GameManager의 모든 Main 키워드 리스트 길이만큼 반복
            for (int i = 0; i < GameManager.instance.allMainKeywordsForPlayer.Count; i++)
            {
                // 오리지널덱의 키워드가 지우고자 하는 키워드와 일치하면
                if (GameManager.instance.allMainKeywordsForPlayer[i].GetComponent<KeywordMain>().keywordName
                    == keyword.GetComponent<KeywordMain>().keywordName)
                {
                    // 보유 골드 HUD에 소지금 차감 및 업데이트
                    UpdateGoldHUD(pricePerKeyword * -1);

                    // 해당 키워드 프리팹을 오리지널 덱 리스트에 추가
                    player.OriginalDeck.AddMainKeywordOnDeck(GameManager.instance.allMainKeywordsForPlayer[i]);
                }
            }
        }
        else
        {
            Debug.LogError("뭐지.. 구매한 키워드가 없다는데?");
        }

        // 구매한 키워드 버튼 비활성화
        UIManager.instance.MakeKeywordInvisible(keyword);
    }

    /// <summary>
    /// 상점의 지우개 버튼 클릭시 발동하는 메소드로, 키워드 제거 메소드의 플로우입니다.
    /// </summary>
    public void UseEraser()
    {
        // 상점 UI 비활성화
        foreach (GameObject tablecloth in shopUI.tablecloths) tablecloth.SetActive(false);
        goldPanel.SetActive(false);

        // 키워드 세팅 북마크로 이동
        Book.instance.EnterKeywordSetting(Keyword.ButtonType.Erase);

        // 마우스 커서 이미지 변경
        GameManager.instance.ChangeCursorImage(CursorType.Eraser);

        // 테이블보 태그 활성화
        DOVirtual.DelayedCall(Book.instance.uIActiveDelay, () => tableclothTag.SetActive(true));
    }

    /// <summary>
    /// 보유중인 키워드를 지웁니다.
    /// </summary>
    public void EraseKeyword(GameObject keyword, Keyword keywordType)
    {
        // Player의 소지금이 부족하면
        if (player.gold < keywordErasingPrice)
        {
            // 버튼 클릭불가 연출 재생
            UIManager.instance.ButtonClicklessFeedback(keyword);

            return;
        }

        // Player의 오리지널덱에 접근하여 키워드 제거
        if (keywordType is KeywordSup) // 지우고자 하는 키워드가 Support 키워드라면
        {
            // Player의 오리지널 Support덱 리스트 길이만큼 반복
            for (int i = 0; i < player.OriginalDeck.SupportDeck.Count; i++)
            {
                // 오리지널덱의 키워드가 지우고자 하는 키워드와 일치하면
                if (player.OriginalDeck.SupportDeck[i].GetComponent<KeywordSup>().keywordName == keyword.GetComponent<KeywordSup>().keywordName)
                {
                    // Player 소지금 업데이트
                    UpdateGoldHUD(keywordErasingPrice * -1);

                    // 해당 키워드를 오리지널 덱 리스트에서 제거
                    player.OriginalDeck.DeleteSpecificKeyword(WhatDeck.SupportDeck, i);

                    // 키워드 버튼 비활성화
                    UIManager.instance.MakeKeywordInvisible(keyword, emptySpaceByErase);

                    return;
                }
            }
        }

        if (keywordType is KeywordMain) // 지우고자 하는 키워드가 Main 키워드라면
        {
            // Player의 오리지널 Main덱 리스트 길이만큼 반복
            for (int i = 0; i < player.OriginalDeck.MainDeck.Count; i++)
            {
                // 오리지널덱의 키워드가 지우고자 하는 키워드와 일치하면
                if (player.OriginalDeck.MainDeck[i].GetComponent<KeywordMain>().keywordName == keyword.GetComponent<KeywordMain>().keywordName)
                {
                    // Player 소지금 업데이트
                    UpdateGoldHUD(keywordErasingPrice * -1);

                    // 해당 키워드를 오리지널 덱 리스트에서 제거
                    player.OriginalDeck.DeleteSpecificKeyword(WhatDeck.MainDeck, i);

                    // 키워드 버튼 비활성화
                    UIManager.instance.MakeKeywordInvisible(keyword, emptySpaceByErase);

                    return;
                }
            }
        }
    }

    /// <summary>
    /// 상품 진열대로 되돌아 가는 메소드입니다.
    /// </summary>
    public void BackToShelves()
    {
        // 인스턴스화된 오리지널 키워드 제거
        Book.instance.DestroyOriginalDeckInfo();

        // 다른 UI 비활성화
        UIManager.instance.ActiveKeywordSettingUI(false);
        tableclothTag.SetActive(false);
        goldPanel.SetActive(false);

        // 페이지 좌로 넘기기 애니메이션 재생
        Book.instance.bookAnimator.SetTrigger("turnPageToLeft");

        // 테이블보 UI 활성화
        DOVirtual.DelayedCall(Book.instance.uIActiveDelay, () => shopUI.tablecloths[0].SetActive(true));
        DOVirtual.DelayedCall(Book.instance.uIActiveDelay, () => shopUI.tablecloths[1].SetActive(true));
        DOVirtual.DelayedCall(Book.instance.uIActiveDelay, () => goldPanel.SetActive(true));

        // 마우스 포인터 변경
        GameManager.instance.ChangeCursorImage(CursorType.Nib);
    }

    /// <summary>
    /// Player의 소지금을 업데이트하는 애니메이션 메소드입니다.
    /// </summary>
    /// <param name="goldDelta">소지금 증감 수치(변동가)를 입력하세요.</param>
    public void UpdateGoldHUD(int goldDelta)
    {
        // Player 소지금에 변동가 업데이트
        player.gold += goldDelta;

        // 보유 골드 HUD에 남은 소지금 업데이트
        goldHUD.DOText(player.gold.ToString(), 1.0f, scrambleMode: ScrambleMode.Numerals).OnUpdate(() =>
        {
            // 소지금 표시창의 끝이 G로 끝나지 않았다면
            if (!goldHUD.text.EndsWith("G"))
            {
                // 끝에 G 텍스트 추가
                goldHUD.text = goldHUD.text.TrimEnd('G') + "G";
            }
        });
    }

    /// <summary>
    /// 상점 퇴장 메소드 플로우입니다.
    /// </summary>
    public void ExitShop()
    {
        // 진열되어 있던 상품 폐기
        shopUI.DisposalKeywordProducts();
        shopUI.DisposalRelicProducts();

        // 상품 진열 여부 false
        areProductsDisplay = false;

        // 상점 UI 비활성화
        UIManager.instance.ActiveShopUI(false);

        // Player 비활성화 및 UI 부분활성화
        player.gameObject.SetActive(false);
        player.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        player.gameObject.transform.GetChild(0).gameObject.SetActive(true);

        // BookTurnL 애니메이션 재생
        Book.instance.bookAnimator.SetTrigger("turnPageToLeft");

        // 맵 UI 활성화
        DOVirtual.DelayedCall(Book.instance.uIActiveDelay, () => UIManager.instance.ActiveMapUI(true));

        // 게임 상태 Map으로 전환
        GameManager.instance.gameState = GameState.Map;
    }
}
