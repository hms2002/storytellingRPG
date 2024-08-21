using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Reward : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public enum RewardType
    {
        keyword,
        relic,
        gold,
        none
    }
    public RewardType rewardType;

    GameObject keywordPrefab;
    public GameObject info;
    public TextMeshProUGUI rewardNameText;
    public string rewardInfoStr;
    public Button button;

    public void SettingReward_Keyword(GameObject _keywordPrefab)
    {
        keywordPrefab = _keywordPrefab;

        // 정보를 가져오기 위해 인스턴스화
        GameObject temp = Instantiate(keywordPrefab);
        temp.SetActive(false);

        // 텍스트 정보, 키워드 프리펩 정보 가져오기
        rewardNameText.text = temp.GetComponent<Keyword>().keywordName;// temp.transform.GetComponentInChildren<TextMeshProUGUI>().text;
        rewardInfoStr = temp.GetComponent<Keyword>().keywordDescription;
        // 메인 키워드면 AddThisToMainDeck()를 본인 버튼 이벤트에 추가
        if (temp.GetComponent<KeywordMain>() != null)
            button.onClick.AddListener(AddThisToMainDeck);
        // 서포트 키워드면 AddThisToSupDeck()를 본인 버튼 이벤트에 추가
        else if (temp.GetComponent<KeywordSup>() != null)
            button.onClick.AddListener(AddThisToSupDeck);

        // 정보 취하고 삭제
        Destroy(temp);
    }

    public void SettingReward_Gold(GameObject _goldBtnPref, int gold)
    {
        // 텍스트 정보, 키워드 프리펩 정보 가져오기
        rewardNameText.text = gold.ToString() + " G";

        button.onClick.AddListener(AddGoldToPlayer);

    }

    public void SettingReward_Relic()
    {
        // 텍스트 정보, 키워드 프리펩 정보 가져오기

        button.onClick.AddListener(AddGoldToPlayer);
        Destroy(gameObject);
    }
    private void OnEnable()
    {
        info.SetActive(false);
    }
    public void AddThisToMainDeck()
    {
        RewardManager.instance.AddMainKeywordToDeck(keywordPrefab);
    }
    public void AddThisToSupDeck()
    {
        RewardManager.instance.AddSupKeywordToDeck(keywordPrefab);
    }
    public void AddGoldToPlayer()
    {
        RewardManager.instance.AddGoldToPlayer();
    }
    public void ClickNoReward()
    {
        RewardManager.instance.ClickNoReward();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        switch(rewardType)
        {
            case RewardType.keyword:
                InfoManager.instance.ShowTipUI(rewardNameText.text, Color.yellow, rewardInfoStr, transform);
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        switch (rewardType)
        {
            case RewardType.keyword:
                InfoManager.instance.HideTipUI();
                break;
        }
    }

    private void OnDestroy()
    {
        switch (rewardType)
        {
            case RewardType.keyword:
                InfoManager.instance.HideTipUI();
                break;
        }
    }
    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    if (onDestroying) return;
    //    if (keyword != null)
    //    {
    //        keyword.ShowInfoUI();
    //    }
    //    if (eventKeyword != null)
    //    {
    //        eventKeyword.ShowInfoUI();
    //    }
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    if (onDestroying) return;
    //    InfoManager.instance.HideTipUI();
    //}
}
