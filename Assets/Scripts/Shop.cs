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
    [Header("테이블보 L, R")]
    [SerializeField] private List<GameObject> _tablecloths;                   // 테이블보 좌, 우 오브젝트를 담는 리스트
    public List<GameObject> tablecloths => _tablecloths;

    [Header("키워드 상품 진열대")]
    [SerializeField] private List<Transform> keywordShelves;               // Support, Main 키워드 상품 진열대

    private List<GameObject> supKeywordProducts = new List<GameObject>();   // Support 키워드 상품 리스트
    private List<GameObject> mainKeywordProducts = new List<GameObject>();  // Main 키워드 상품 리스트


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
            supKeywordProducts.Add(Instantiate(KeywordProductSelection(WhatDeck.SupportDeck), keywordShelves[0])); 

            // 키워드 버튼 클릭타입 전환
            supKeywordProducts[i].GetComponent<Keyword>().buttonType = Keyword.ButtonType.Purchase;

            // 키워드 스케일 축소
            supKeywordProducts[i].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

            // 키워드 각조 조절
            supKeywordProducts[i].transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(-3.0f, 3.0f));
            supKeywordProducts[i].transform.GetChild(0).rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

            // Support 키워드 박스 활성화
            supKeywordProducts[i].transform.Find("SupKeywordBox").gameObject.SetActive(true);
        }

        // Main 키워드 상품 발주량만큼 반복
        for (int i = 0; i < ShopManager.instance.orderVolume; i++)
        {
            // 랜덤 발주한 키워드 인스턴스화 및 진열
            mainKeywordProducts.Add(Instantiate(KeywordProductSelection(WhatDeck.MainDeck), keywordShelves[1]));

            // 키워드 버튼 클릭타입 전환
            mainKeywordProducts[i].GetComponent<Keyword>().buttonType = Keyword.ButtonType.Purchase;

            // 키워드 스케일 축소
            mainKeywordProducts[i].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

            // 키워드 각조 조절
            mainKeywordProducts[i].transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(-3.0f, 3.0f));
            mainKeywordProducts[i].transform.GetChild(0).rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

            // Main 키워드 박스 활성화
            mainKeywordProducts[i].transform.Find("MainKeywordBox").gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 키워드 타입에 맞게 상품 한 개를 랜덤 발주합니다.
    /// </summary>
    /// <param name="thisDeck">랜덤 발주할 키워드의 타입을 입력하세요. (Support 혹은 Main)</param>
    /// <returns>랜덤으로 발주된 키워드를 반환한다.</returns>
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
        foreach (GameObject keywordProduct in supKeywordProducts)
        {
            //
            Destroy(keywordProduct);
        }

        //
        supKeywordProducts.Clear();

        //
        foreach (GameObject keywordProduct in mainKeywordProducts)
        {
            //
            Destroy(keywordProduct);
        }

        //
        mainKeywordProducts.Clear();
    }
}
