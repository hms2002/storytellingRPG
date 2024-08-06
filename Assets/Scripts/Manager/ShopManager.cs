using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 상점 시스템 흐름 제어
/// </summary>
public class ShopManager : MonoBehaviour
{


    /*==================================================================================================================================*/


    public void EnterShop()
    {
        // gameState가 Map이거나 Battle이면 return
        if (GameManager.instance.gameState == GameState.Map || GameManager.instance.gameState == GameState.Battle) return;

        // 다른 UI 비활성화
        UIManager.instance.ActiveMapUI(false);

        // 페이지 넘기기 애니메이션
        Book.instance.bookAnimator.SetTrigger("turnPageRight");

        // 상점 UI 활성화
        DOVirtual.DelayedCall(Book.instance.UIActiveDelay, () => UIManager.instance.ActiveShopUI(true));

        // 
    }
}
