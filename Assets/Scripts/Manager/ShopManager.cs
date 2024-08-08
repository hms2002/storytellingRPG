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
    public static ShopManager instance;

    [Header("ShopUI 오브젝트")]
    [SerializeField] private Shop shop;

    [Header("플레이어 오브젝트")]
    [SerializeField] private Actor player;

    [Header("골드 UI 오브젝트")]
    [SerializeField] private TextMeshProUGUI goldHUD;

    [Header("키워드 가격표 오브젝트")]
    [SerializeField] private TextMeshProUGUI keywordPrice;


    [Space(40)]


    [Header("키워드 상품 발주량")]
    [SerializeField] private int _orderVolume = 6;               // 키워드 상품 발주량
    public int orderVolume { get => _orderVolume; set => _orderVolume = value; }

    [Header("키워드 개당 가격")]
    [SerializeField] private int howMuch = 50;


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
        keywordPrice.text = howMuch.ToString() + "G";

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
        if (player.gold < howMuch)
        {
            // 돈이 부족합니다
            UIManager.instance.ButtonClicklessFeedback(keyword);

            return;
        }

        // 플레이어 소지 금액에서 키워드 가격만큼 차감
        player.gold -= howMuch;

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

        // 플레이어의 오리지널덱에 추가
        if (keywordType is KeywordSup) // 키워드 타입이 Support라면
        {
            // 플레이어의 오리지널 Support덱에 구매한 키워드 추가
            player.AddSupKeywordToOriginalDeck(keyword);
        }
        else if (keywordType is KeywordMain) // 키워드 타입이 Main이라면
        {
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
    /// 상점 퇴장 메소드 플로우입니다.
    /// </summary>
    public void ExitShop()
    {
        // 진열되어 있던 키워드 상품 폐기
        shop.DisposalKeywordProducts();
    }
}
