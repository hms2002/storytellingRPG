using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 상점 시스템 흐름 제어
/// </summary>
public class ShopManager : MonoBehaviour
{
    [Header("ShopUI 오브젝트")]
    [SerializeField] private Shop shop;


    /*==================================================================================================================================*/


    /// <summary>
    /// 상점 입장 메소드의 플로우
    /// </summary>
    public void EnterShop()
    {
        // gameState가 Map이거나 Battle이면 return
        if (GameManager.instance.gameState == GameState.Map || GameManager.instance.gameState == GameState.Battle) return;

        // 다른 UI 비활성화
        UIManager.instance.ActiveMapUI(false);

        // 페이지 넘기기 애니메이션
        Book.instance.bookAnimator.SetTrigger("turnPageToRight");

        // 상점 UI 활성화
        DOVirtual.DelayedCall(Book.instance.UIActiveDelay, () => UIManager.instance.ActiveShopUI(true));

        // 키워드 발주 및 진열
        shop.KeywordProductsDisplay();

        // 유물 발주 및 진열

    }
}
