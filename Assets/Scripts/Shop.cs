using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 상점 내부 시스템 제어
/// </summary>
public class Shop : MonoBehaviour
{
    [Header("키워드 상품 진열대")]
    [SerializeField] private List<Transform> KeywordShelves;   // Support, Main 키워드 상품 진열대

    private List<GameObject> SupKeywordProducts;               // Support 키워드 상품 리스트
    private List<GameObject> MainKeywordProducts;             // Main 키워드 상품 리스트


    /*==================================================================================================================================*/


    /// <summary>
    /// 랜덤 발주한 키워드를 키워드 상품 진열대에 진열한다.
    /// </summary>
    public void KeywordProductsDisplay()
    {
        // Support 키워드 상품 발주량만큼 반복
        for (int i = 0; i < ShopManager.instance.orderVolume; i++)
        {
            // 랜덤 발주한 키워드 인스턴스화 및 진열
            SupKeywordProducts[i] = GameObject.Instantiate(KeywordProductSelection(WhatDeck.SupportDeck), KeywordShelves[0]);

            // 키워드 버튼 클릭타입 전환
            SupKeywordProducts[i].GetComponent<Keyword>().buttonType = Keyword.ButtonType.Purchase;

            // 키워드 스케일 축소
            SupKeywordProducts[i].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

            // 키워드 각조 조절
            SupKeywordProducts[i].transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(-3.0f, 3.0f));
            SupKeywordProducts[i].transform.GetChild(0).rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

            // Support 키워드 박스 활성화
            SupKeywordProducts[i].transform.Find("SupKeywordBox").gameObject.SetActive(true);
        }

        // Main 키워드 상품 발주량만큼 반복
        for (int i = 0; i < ShopManager.instance.orderVolume; i++)
        {
            // 랜덤 발주한 키워드 인스턴스화 및 진열
            MainKeywordProducts[i] = GameObject.Instantiate(KeywordProductSelection(WhatDeck.MainDeck), KeywordShelves[1]);

            // 키워드 버튼 클릭타입 전환
            MainKeywordProducts[i].GetComponent<Keyword>().buttonType = Keyword.ButtonType.Purchase;

            // 키워드 스케일 축소
            MainKeywordProducts[i].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

            // 키워드 각조 조절
            MainKeywordProducts[i].transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(-3.0f, 3.0f));
            MainKeywordProducts[i].transform.GetChild(0).rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

            // Main 키워드 박스 활성화
            MainKeywordProducts[i].transform.Find("MainKeywordBox").gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 키워드 타입에 맞게 상품 한 개를 랜덤 발주합니다.
    /// </summary>
    /// <param name="thisDeck">랜덤 발주할 키워드의 타입을 입력하세요. (Support 혹은 Main)</param>
    /// <returns></returns>
    private GameObject KeywordProductSelection(WhatDeck thisDeck)
    {
        GameObject keywordToReturn = new GameObject();  // 리턴할 키워드 상품을 잠시 담아둘 변수
        int maxRange;                                   // 랜덤값 최대 범위

        switch (thisDeck)
        {
            case WhatDeck.SupportDeck:

                // 플레이어가 가질 수 있는 모든 Support 키워드의 리스트 길이로 최대 범위 설정
                maxRange = GameManager.instance.allSupKeywordsForPlayer.Count;

                // Support 키워드 리스트에서 프리팹 한 개를 랜덤 발주
                keywordToReturn = GameManager.instance.allSupKeywordsForPlayer[Random.Range(0, maxRange)];

                break;

            case WhatDeck.MainDeck:

                // 플레이어가 가질 수 있는 모든 Main 키워드의 리스트 길이로 최대 범위 설정
                maxRange = GameManager.instance.allMainKeywordsForPlayer.Count;

                // Main 키워드 리스트에서 프리팹 한 개를 랜덤 발주
                keywordToReturn = GameManager.instance.allMainKeywordsForPlayer[Random.Range(0, maxRange)];

                break;
        }

        return keywordToReturn;
    }

    /// <summary>
    /// 진열되어 있던 키워드 상품들을 폐기처분합니다.
    /// </summary>
    public void DisposalKeywordProducts()
    {
        //
        foreach (GameObject keywordProduct in SupKeywordProducts)
        {
            //
            Destroy(keywordProduct);
        }

        //
        SupKeywordProducts.Clear();

        //
        foreach (GameObject keywordProduct in MainKeywordProducts)
        {
            //
            Destroy(keywordProduct);
        }

        //
        MainKeywordProducts.Clear();
    }
}
