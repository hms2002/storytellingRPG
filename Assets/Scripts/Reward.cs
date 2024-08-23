using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

public class Reward : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public enum RewardType
    {
        keyword,
        relic,
        gold,
        none,
        skip
    }
    public RewardType rewardType;

    GameObject keywordPrefab;
    public GameObject info;
    public TextMeshProUGUI rewardNameText;
    public string rewardInfoStr;
    public Button button;
    public Image RelicImage;
    Color KeywordColor;

    private void OnEnable()
    {
    }

    public void SettingReward_Keyword(GameObject _keywordPrefab)
    {
        keywordPrefab = _keywordPrefab;

        // 정보를 가져오기 위해 인스턴스화
        GameObject temp = Instantiate(keywordPrefab);
        temp.SetActive(false);

        // 텍스트 정보, 키워드 프리펩 정보 가져오기
        Debug.Log(temp);
        Debug.Log(temp.GetComponent<Keyword>());
        Debug.Log(temp.GetComponent<Keyword>().keywordName);
        rewardNameText.text = temp.GetComponent<Keyword>().keywordName;// temp.transform.GetComponentInChildren<TextMeshProUGUI>().text;
        rewardInfoStr = temp.GetComponent<Keyword>().FormatDescription(temp.GetComponent<Keyword>().keywordDescription);
        KeywordColor = temp.GetComponent<Keyword>().GetKeywordColor();
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
        button.onClick.AddListener(() => Destroy(gameObject));
    }

    GameObject tempRelicModel;
    /// <summary>
    /// 보상으로 나오는 유물 버튼을 세팅합니다.
    /// </summary>
    /// <param name="relicData">설정할 세팅값 참조를 위해 유물 데이터를 받아옵니다.</param>
    public void SettingReward_Relic(GameObject dataObj, PlayerRelic playerRelic)
    {

        RelicData data = dataObj.GetComponent<Relic>().relicData;
        RelicImage.sprite = data.relicImage;
        rewardNameText.text = data.RelicName;
        rewardInfoStr = data.RelicDescription;

        tempRelicModel = Instantiate(dataObj);
        tempRelicModel.SetActive(false);

        button.onClick.AddListener(() => playerRelic.AddRelicForInstantciated(tempRelicModel));
        button.onClick.AddListener(() => RewardManager.instance.AddRelicToPlayer(data));
        button.onClick.AddListener(() => Destroy(gameObject));
    }
    public void SettingSkipReward()
    {
        button.onClick.AddListener(() => ClickSkipReward());
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
    public void ClickSkipReward()
    {
        RewardManager.instance.ClickSkipButton();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        switch(rewardType)
        {
            case RewardType.keyword:
                InfoManager.instance.ShowTipUI(rewardNameText.text, KeywordColor, rewardInfoStr, transform);
                break;
            case RewardType.relic:
                InfoManager.instance.ShowTipUI(rewardNameText.text, Color.black, rewardInfoStr, transform);
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
            case RewardType.relic:
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
            case RewardType.relic:
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
