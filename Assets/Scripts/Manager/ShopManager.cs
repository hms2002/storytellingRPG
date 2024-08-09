using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 상점 시스템 흐름 제어
/// </summary>
public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;                     // 싱글톤

    [Header("ShopUI 오브젝트")]
    [SerializeField] private Shop shop;                     // ShopUI 오브젝트의 Shop 스크립트 컴포넌트

    [Header("플레이어 오브젝트")]
    [SerializeField] private Actor player;                  // Player 오브젝트의 Actor 스크립트 컴포넌트

    [Header("골드 Panel")]
    [SerializeField] private GameObject _goldPanel;         //
    public GameObject goldPanel => _goldPanel;

    [Header("골드 TextMeshPro 오브젝트")]
    [SerializeField] private TextMeshProUGUI goldHUD;       // 

    [Header("키워드 가격표 오브젝트")]
    [SerializeField] private TextMeshProUGUI keywordPrice;  // 

    [Header("키워드 제거 시 교체될 이미지")]
    [SerializeField] private Sprite emptySpaceByErase;      // 


    [Space(40)]


    [Header("키워드 상품 발주량")]
    [SerializeField] private int _orderVolume = 6;          // 키워드 상품 발주량
    public int orderVolume { get => _orderVolume; set => _orderVolume = value; }

    [Header("키워드 개당 가격")]
    [SerializeField] private int pricePerKeyword = 50;

    [Header("키워드 제거 가격")]
    [SerializeField] private int keywordErasingPrice = 120;


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

        // 다른 UI 비활성화
        UIManager.instance.ActiveMapUI(false);

        // 페이지 넘기기 애니메이션
        Book.instance.bookAnimator.SetTrigger("turnPageToRight");

        // 상점 UI 활성화 및 플레이어 오브젝트 활성화
        DOVirtual.DelayedCall(Book.instance.UIActiveDelay, () => UIManager.instance.ActiveShopUI(true));
        DOVirtual.DelayedCall(Book.instance.UIActiveDelay, () => player.gameObject.SetActive(true));

        // 키워드 가격 표시
        keywordPrice.text = "단돈 " + pricePerKeyword.ToString() + "G!";

        // 플레이어 소지금 표기
        goldHUD.DOText(player.gold + "G", 0.0f);

        // 키워드 발주 및 진열
        shop.KeywordProductsDisplay();

        // 유물 발주 및 진열
        //shop.RelicProductsDisplay();
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

        // 플레이어 소지 금액에서 키워드 가격만큼 차감
        player.gold -= pricePerKeyword;

        // 보유 골드 HUD에 남은 소지금 업데이트
        UpdateGoldHUD();

        // 플레이어의 오리지널덱에 추가
        if (keywordType is KeywordSup) // 키워드 타입이 Support라면
        {
            Debug.Log("사졌니?");
            // 플레이어의 오리지널 Support덱에 구매한 키워드 추가
            player.AddSupKeywordToOriginalDeck(keyword);
        }
        else if (keywordType is KeywordMain) // 키워드 타입이 Main이라면
        {
            Debug.Log("사졌니?");
            // 플레이어의 오리지널 Main덱에 구매한 키워드 추가
            player.AddSupKeywordToOriginalDeck(keyword);
        }
        else
        {
            Debug.LogError("뭐지.. 구매한 키워드가 없다는데?");
        }

        // 키워드 버튼 비활성화
        UIManager.instance.MakeKeywordInvisible(keyword);
    }

    /// <summary>
    /// 상점의 지우개 버튼 클릭시 발동하는 메소드로, 키워드 제거 메소드의 플로우입니다.
    /// </summary>
    public void UseEraser()
    {
        // 상점 UI 비활성화
        shop.tablecloths[0].SetActive(false);
        shop.tablecloths[1].SetActive(false);
        goldPanel.SetActive(false);


        // 키워드 세팅 북마크로 이동
        Book.instance.EnterKeywordSetting(Keyword.ButtonType.Erase);

        // 마우스 커서 이미지 변경


        //
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

        // 지우고자 하는 키워드가 Support 키워드라면
        if (keywordType is KeywordSup)
        {
            //
            for (int i = 0; i < player.OriginalDeck.SupportDeck.Count; i++)
            {
                // 오리지널덱의 키워드가 지우고자 하는 키워드와 일치하면
                if (player.OriginalDeck.SupportDeck[i].GetComponent<KeywordSup>().keywordName == keyword.GetComponent<KeywordSup>().keywordName)
                {
                    // Player 소지금 업데이트
                    player.gold -= keywordErasingPrice;
                    UpdateGoldHUD();

                    // 해당 키워드를 오리지널 덱 리스트에서 제거
                    player.OriginalDeck.DeleteSpecificKeyword(WhatDeck.SupportDeck, i);

                    // 키워드 버튼 비활성화
                    UIManager.instance.MakeKeywordInvisible(keyword, emptySpaceByErase);

                    return;
                }
            }
        }

        // 지우고자 하는 키워드가 Main 키워드라면
        if (keywordType is KeywordMain)
        {
            for (int i = 0; i < player.OriginalDeck.MainDeck.Count; i++)
            {
                // 오리지널덱의 키워드가 지우고자 하는 키워드와 일치하면
                if (player.OriginalDeck.MainDeck[i].GetComponent<KeywordMain>().keywordName == keyword.GetComponent<KeywordMain>().keywordName)
                {
                    // Player 소지금 업데이트
                    player.gold -= keywordErasingPrice;
                    UpdateGoldHUD();

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
    /// Player의 소지금을 업데이트하는 애니메이션 메소드입니다.
    /// </summary>
    private void UpdateGoldHUD()
    {
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
        // 진열되어 있던 키워드 상품 폐기
        shop.DisposalKeywordProducts();
    }
}
