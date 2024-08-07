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

    [SerializeField] private int orderVolume = 6;               // 키워드 상품 발주량


    /*==================================================================================================================================*/


    /// <summary>
    /// 랜덤 발주한 키워드를 키워드 상품 진열대에 진열한다.
    /// </summary>
    public void KeywordProductsDisplay()
    {
        GameObject TempForSettingKeyword = new GameObject();  // 키워드 세팅용 임시 변수

        // Support 키워드 상품 발주량만큼 반복
        for (int i = 0; i < orderVolume; i++)
        {
            // 랜덤 발주한 키워드 인스턴스화 및 진열
            TempForSettingKeyword = GameObject.Instantiate(KeywordProductSelection(WhatDeck.SupportDeck), KeywordShelves[0]);

            // 키워드 버튼 비활성화
            TempForSettingKeyword.GetComponent<Button>().enabled = false;

            // 키워드 스케일 축소
            TempForSettingKeyword.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

            // Support 키워드 박스 활성화
            TempForSettingKeyword.transform.Find("SupKeywordBox").gameObject.SetActive(true);
        }

        // Main 키워드 상품 발주량만큼 반복
        for (int i = 0; i < orderVolume; i++)
        {
            // 랜덤 발주한 키워드 인스턴스화 및 진열
            TempForSettingKeyword = GameObject.Instantiate(KeywordProductSelection(WhatDeck.MainDeck), KeywordShelves[1]);

            // 키워드 버튼 비활성화
            TempForSettingKeyword.GetComponent<Button>().enabled = false;

            // 키워드 스케일 축소
            TempForSettingKeyword.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

            // Main 키워드 박스 활성화
            TempForSettingKeyword.transform.Find("MainKeywordBox").gameObject.SetActive(true);
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
}
