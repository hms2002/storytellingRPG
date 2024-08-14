using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 전투 시 활성화되는 기본덱, 무덤덱의 정보를 제어
/// </summary>
public class DeckInfoPivot : MonoBehaviour
{
    [Header("덱 정보 피벗")]
    [SerializeField] private GameObject deckInfoPivot;  // DeckInfoPivot 자신
    [SerializeField] private GameObject info;           // Info 오브젝트

    [SerializeField] private List<GameObject> _deckInfo = new List<GameObject>();    // Actor로부터 덱 정보를 받아와 담는 리스트
    public IReadOnlyList<GameObject> deckInfo => _deckInfo;

    private Image infoBackground;                       // 덱 정보 피벗 UI의 뒷배경

    private bool _arePlayerChoosingKeyword = false;     // 플레이어가 키워드를 선택중인지에 대한 여부
    public  bool arePlayerChoosingKeyword { get => _arePlayerChoosingKeyword; set => _arePlayerChoosingKeyword = value; }

    private bool areKeywordsInstanciate = false;        // 키워드 정보 인스턴트화 여부

    //=============================================================================================================

    private void OnEnable()
    {
        // DeckInfoPivot의 하위 객체인 Info 객체의 이미지 컴포넌트를 참조
        infoBackground = info.GetComponent<Image>();
    }

    /// <summary>
    /// DeckInfoPivot이 갖고 있는 deckInfo 리스트에 키워드 프리팹 정보를 받아온다.
    /// </summary>
    /// <param name="deckPrefabList">덱 프리팹 리스트</param>
    public void RecieveDeckInfo(IReadOnlyList<GameObject> deckPrefabList)
    {
        // 받아온 덱 프리팹 리스트의 길이만큼 반복
        for (int i = 0; i < deckPrefabList.Count; i++)
        {
            // deckInfo 리스트에 프리팹 추가
            _deckInfo.Add(deckPrefabList[i]);
        }
    }

    /// <summary>
    /// DeckInfoPivot 버튼을 클릭시 키워드를 보여준다.
    /// </summary>
    public void ClickDeckInfo()
    {
        // 키워드 고르는 중이 아니라면
        if (!arePlayerChoosingKeyword)
        {
            // 버튼 클릭불가 연출 실행
            UIManager.instance.ButtonClicklessFeedback(this.gameObject);

            return;
        }

        // DeckInfoPivotUI가 Hierarchy상에서 활성화되어 있다면
        if (info.activeInHierarchy)
        {
            // info 오브젝트 비활성화
            info.SetActive(false);

            return;
        }

        // 키워드들이 인스턴스화되어 있다면
        if (areKeywordsInstanciate)
        {
            // deckInfoPivot 오브젝트를 가장 나중에 출력하도록 설정
            deckInfoPivot.GetComponent<RectTransform>().SetAsLastSibling();

            // info 오브젝트 활성화
            info.SetActive(true);

            return;
        }

        // deckInfoPivot 오브젝트를 가장 나중에 출력하도록 설정
        deckInfoPivot.GetComponent<RectTransform>().SetAsLastSibling();

        // info 오브젝트 활성화
        info.SetActive(true);

        // 덱 정보 인스턴스화
        makeDeckInfo();
    }

    /// <summary>
    /// deckInfo 리스트의 키워드들을 인스턴스화한다.
    /// </summary>
    private void makeDeckInfo()
    {
        GameObject keywordTemp;     // 인스턴스화된 키워드를 잠시 담아놓을 변수

        // 키워드 인스턴스화 및 설정
        for (int i = 0; i < _deckInfo.Count; i++)
        {
            // i번째 키워드 인스턴스화
            keywordTemp = Instantiate(_deckInfo[i], info.transform);

            // 키워드 버튼 컴포넌트의 상호작용 비활성화
            keywordTemp.GetComponent<Button>().interactable = false;
        }

        // 키워드 인스턴스화 여부 true 설정
        areKeywordsInstanciate = true;
    }
    

    /// <summary>
    /// 턴이 종료되면 deckInfo 리스트를 초기화한다.
    /// </summary>
    public void ClearDeckInfo()
    {
        // 인스턴스화한 키워드 정보가 있다면
        if (info.transform.childCount >= 0)
        {
            // info 객체의 하위 객체 개수만큼 인스턴스 제거 반복
            for (int i = 0; i < info.transform.childCount; i++)
            {
                Destroy(info.transform.GetChild(i).gameObject);
            }
        }

        // 덱 정보 리스트 초기화
        _deckInfo.Clear();

        // 키워드 인스턴스화 여부 false 설정
        areKeywordsInstanciate = false;
    }
}
